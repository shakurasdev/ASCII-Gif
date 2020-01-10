using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ASCIITools
{
    public class PicConverter
    {
        public int Teiler { get; set; }
        public int YTeiler { get { return (int)(Teiler * 2.2); } }

        private readonly double _redPart = 0.299;
        private readonly double _greenPart = 0.587;
        private readonly double _bluePart = 0.114;

        public bool SaveTxt { get; set; }

        public ASCIIWeightCalc WeightCalc { get; private set; }

        public PicConverter(int teiler, int fontsize, ASCIICategories categories) : this(teiler, fontsize, categories, false)
        {
            
        }
        public PicConverter(int teiler, int fontsize, ASCIICategories categories, bool savetxt)
        {
            Teiler = teiler;
            WeightCalc = new ASCIIWeightCalc(fontsize, categories);
            SaveTxt = savetxt;
        }

        private IEnumerable<int> PixelBoxSums(Bitmap bmp, int startX, int startY)
        {
            for (int y = startY; y < startY + YTeiler; y++)
            {
                for (int x = startX; x < startX + Teiler; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    yield return (int)(c.R * _redPart + c.G * _greenPart + c.B * _bluePart);
                }
            }
        }

        private int[] ZeilenWerte(int zeile, int spalten, Bitmap bmp)
        {
            /*
             zeile als boxen vom bild betrachtet zurückgegeben
             */

            int[] vs = new int[spalten];

            for (int i = 0; i < spalten; i++)
            {
                vs[i] = (int)PixelBoxSums(bmp, i * Teiler, zeile * YTeiler).Average();
            }
            return vs;
        }

        public string[] BitmapToTextRows(Bitmap bmp)
        {
            int x = bmp.Width / Teiler; //spalten
            int y = bmp.Height / YTeiler; //zeilen

            List<int[]> zeilen = new List<int[]>();

            //loop durch zeilen
            for (int i = 0; i < y; i++)
            {
                zeilen.Add(ZeilenWerte(i, x, bmp));
            }

            string[] ergebnis = new string[zeilen.Count];
            for (int i = 0; i < zeilen.Count; i++)
            {
                ergebnis[i] = ArrayToChars(zeilen[i]);
            }

            //txt speichern
            if (SaveTxt)
            {
                using (StreamWriter writer = new StreamWriter(Application.StartupPath + @"\ergebnis.txt"))
                {
                    for (int i = 0; i < ergebnis.Length; i++)
                    {
                        writer.WriteLine(ergebnis[i]);
                    }
                    writer.Close();
                }
            }

            return ergebnis;
        }

        private string ArrayToChars(int[] werte)
        {
            string s = "";

            for (int i = 0; i < werte.Length; i++)
            {
                s += WertAlsChar(werte[i]);
            }

            return s;
        }
                
        private char WertAlsChar(int wert)
        {
            double steps = 256 / (double)WeightCalc.CharsWeighted.Count;

            int itemx = (int)(wert / steps);

            return WeightCalc.CharsWeighted.ElementAt(itemx);
            
        }
    }
}
