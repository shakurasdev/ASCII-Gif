using System.Drawing;

namespace ASCIITools
{
    public class BitmapFontSizeCalc
    {
        private readonly Point _textCoord;
        private Font _font;
        private int _fontsize;

        public double FontWidth { get; private set; }
        public double FontHeight { get; private set; }

        public BitmapFontSizeCalc(int fontsize, Font font)
        {
            _fontsize = fontsize;
            _textCoord = new Point(0, 0);
            _font = font;

            TestBitmapFont();
        }
             
        private void TestBitmapFont()
        {
            Bitmap bmp = GenerateTestBitmap();

            double xborder = 0;
            double yborder = 0;

            for (int y = bmp.Height - 1; y > 0; y--)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    if (c.B > 0)
                    {
                        yborder = (y + 1) / 1.8; //durch anzahl zeilen im teststring minus 0.2
                        break;
                    }
                }
                if (yborder != 0) break;
            }
            for (int x = bmp.Width - 1; x > 0; x--)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    if (c.B > 0)
                    {
                        xborder = (x) / 4.0; //durch anzahl zeichen per zeile im teststring
                        break;
                    }
                }
                if (xborder != 0) break;
            }

            FontWidth = xborder;
            FontHeight = yborder;
        }

        private Bitmap GenerateTestBitmap()
        {
            Bitmap bmp = new Bitmap(_fontsize * 4, _fontsize * 4);

            string teststring = "KMKM" + "\r\n" + "pqpq"; //"▓▓" + "\r\n" + "▓▓"

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(255, 0, 0));
                g.DrawString(teststring, _font, new SolidBrush(Color.FromArgb(0, 0, 255)), _textCoord);
                //bmp.Save("fonttest.png", ImageFormat.Png);
                return bmp;
            }
        }



    }
}
