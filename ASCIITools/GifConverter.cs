using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ASCIITools
{
    public class GifConverter
    {
        public Font Font { get; }

        private PicConverter _converter;
        private BitmapFontSizeCalc _bitmapFontSizeCalc;

        private Image _gif;
        public Image Gif
        {
            get { return _gif; }
            set
            {
                _gif = value;
                _currentFrame = 0;
                _totalFrames = _gif.GetFrameCount(FrameDimension.Time);
            }
        }
        
        private int _currentFrame;
        private int _totalFrames;

        public int TotalFrames { get { return _totalFrames; } }

        private int _outputWidth;
        private int _outputHeight;

        private int _cropX;
        private int _cropY;

        public GifConverter(Image gif, int teiler, int fontsize, ASCIICategories categories)
        {
            Gif = gif;

            Font = new Font(FontFamily.GenericMonospace, fontsize);
            _converter = new PicConverter(teiler, fontsize, categories);
            _bitmapFontSizeCalc = new BitmapFontSizeCalc(fontsize, Font);
        }


        #region DetermineCropXY
        /**
         * checks for the first non-white pixel on x/y-axis
         */
        private void DetermineCropXY(Bitmap bitmap)
        {
            _cropX = 0;
            _cropY = 0;

            for (int y = bitmap.Height - 1; y > 0; y--)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color c = bitmap.GetPixel(x, y);
                    if (c.R < 255 || c.G < 255 || c.B < 255)
                    {
                        _cropY = (y + 1);
                        break;
                    }
                }
                if (_cropY != 0) break;
            }
            for (int x = bitmap.Width - 1; x > 0; x--)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color c = bitmap.GetPixel(x, y);
                    if (c.R < 255 || c.G < 255 || c.B < 255)
                    {
                        _cropX = (x + 1);
                        break;
                    }
                }
                if (_cropX != 0) break;
            }
        }
        #endregion


        #region GetNextFrame
        public Bitmap GetNextFrame()
        {
            if (_currentFrame < _totalFrames)
            {
                //convert frame to ascii and draw it
                _gif.SelectActiveFrame(FrameDimension.Time, _currentFrame);
                Bitmap frame = new Bitmap(_gif);
                string[] frameZeilen = _converter.BitmapToTextRows(frame);

                if (_currentFrame == 0)
                {
                    _outputWidth = (int)(_bitmapFontSizeCalc.FontWidth * frameZeilen[0].Length);
                    _outputHeight = (int)(_bitmapFontSizeCalc.FontHeight * frameZeilen.Count());
                }
                
                Bitmap frameUncroppedAscii = new Bitmap(_outputWidth, _outputHeight);
                Graphics g = Graphics.FromImage(frameUncroppedAscii);
                g.Clear(Color.White);
                g.DrawString(string.Join("\r\n", frameZeilen), Font, Brushes.Black, 0, 0);
                g.Dispose();

                //crop ascii frame to remove potruding edges
                if (_currentFrame == 0)
                {
                    DetermineCropXY(frameUncroppedAscii);
                }

                Bitmap cropPng = new Bitmap(_cropX, _cropY);
                Graphics gc = Graphics.FromImage(cropPng);
                gc.DrawImage(frameUncroppedAscii, 0, 0);

                //return it and clean up
                frameUncroppedAscii.Dispose();
                gc.Dispose();
                _currentFrame++;
                return cropPng;
            }
            else
            {
                return null;
            }
        }
        #endregion

        private PropertyItem CreateFrameDelayPropertyItem()
        {
            // PropertyItem for the delay between each frame.
            // https://docs.microsoft.com/de-de/dotnet/api/system.drawing.imaging.propertyitem.id?view=netframework-4.7.2#System_Drawing_Imaging_PropertyItem_Id
            return _gif.PropertyItems.FirstOrDefault(pi => pi.Id == 0x5100);
        }
        private PropertyItem CreateLoopPropertyItem()
        {
            // PropertyItem for the number of animation loops.
            // https://docs.microsoft.com/de-de/dotnet/api/system.drawing.imaging.propertyitem.id?view=netframework-4.7.2#System_Drawing_Imaging_PropertyItem_Id
            return _gif.PropertyItems.FirstOrDefault(pi => pi.Id == 0x5101);

            /* for reference - how to create this item from scratch:
             * https://stackoverflow.com/questions/17781180/how-to-create-a-gif-image-with-c-sharp/28148608
             
            // https://docs.microsoft.com/de-de/dotnet/api/system.drawing.imaging.propertyitem?view=netframework-4.7.2
             * A PropertyItem is not intended to be used as a stand-alone object. 
             * A PropertyItem object is intended to be used by classes that are derived from Image. 
             * A PropertyItem object is used to retrieve and to change the metadata of existing image files, not to create the metadata. 
             * Therefore, the PropertyItem class does not have a defined Public constructor, and you cannot create an instance of a PropertyItem object. 
             * 
             * ... das machen wir aber trotzdem und zwar so:
             
            var loopPropertyItem = (PropertyItem)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(PropertyItem));

            // 0x5101 - PropertyTagLoopCount
            // https://docs.microsoft.com/de-de/dotnet/api/system.drawing.imaging.propertyitem.id?view=netframework-4.7.2#System_Drawing_Imaging_PropertyItem_Id
            loopPropertyItem.Id = 0x5101;
            // 1 - Specifies that Value is an array of bytes.
            // muss wohl 3 sein, weil ein einzelnes byte für die byte-array einstellung nicht passt
            // 3 - Specifies that Value is an array of unsigned short (16-bit) integers.
            // https://docs.microsoft.com/de-de/dotnet/api/system.drawing.imaging.propertyitem.type?view=netframework-4.7.2#System_Drawing_Imaging_PropertyItem_Type
            loopPropertyItem.Type = 3;

            // An integer that represents the length (in bytes) of the Value byte array.
            // https://docs.microsoft.com/de-de/dotnet/api/system.drawing.imaging.propertyitem.len?view=netframework-4.7.2#System_Drawing_Imaging_PropertyItem_Len
            // value is a single byte == 0
            loopPropertyItem.Len = 1;

            // A byte array that represents the value of the property item
            // https://docs.microsoft.com/de-de/dotnet/api/system.drawing.imaging.propertyitem.value?view=netframework-4.7.2#System_Drawing_Imaging_PropertyItem_Value
            // 0 means to animate forever.
            // könnte auch short cast sein -> siehe type = 3
            //  aber type = 1 und byte cast funktioniert nicht
            loopPropertyItem.Value = BitConverter.GetBytes((byte)0);
            */
        }

        #region CreateGifAtCurrentFrame
        public void CreateGifAtCurrentFrame(BackgroundWorker bw, string filename)
        {
            //von hier zusammengeschnipselt https://stackoverflow.com/questions/17781180/how-to-create-a-gif-image-with-c-sharp/28148608

            ImageCodecInfo gifEncoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Gif.Guid);

            
            var loopPropertyItem = CreateLoopPropertyItem();

            var frameDelayPropertyItem = CreateFrameDelayPropertyItem();

            //to create new file as an animated gif
            var paramsFrame1 = new EncoderParameters(1);
            paramsFrame1.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.MultiFrame);

            //indicates these frames are part of the animation loop
            var paramsFrameN = new EncoderParameters(1);
            paramsFrameN.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.FrameDimensionTime);

            //flush at the end
            var paramsEncoderFlush = new EncoderParameters(1);
            paramsEncoderFlush.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.Flush);

            try
            {
                using (var stream = new FileStream(filename, FileMode.Create))
                {
                    Bitmap firstBitmap = GetNextFrame();

                    firstBitmap.SetPropertyItem(loopPropertyItem);
                    firstBitmap.SetPropertyItem(frameDelayPropertyItem);
                    firstBitmap.Save(stream, gifEncoder, paramsFrame1);

                    Bitmap currentBitmap = GetNextFrame();
                    while (currentBitmap != null)
                    {
                        firstBitmap.SaveAdd(currentBitmap, paramsFrameN);
                        
                        bw.ReportProgress(_currentFrame);

                        currentBitmap.Dispose();
                        currentBitmap = GetNextFrame();
                    }
                    
                    firstBitmap.SaveAdd(paramsEncoderFlush);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Error while creating output file.");
            }
        }
        #endregion

        
    }
}
