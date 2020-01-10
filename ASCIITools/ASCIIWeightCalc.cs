using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ASCIITools
{
    public class ASCIIWeightCalc
    {
        private readonly Point _textCoord;
        private readonly Font _font;
        private readonly int _fontsize;

        public List<char> CharsWeighted { get; private set; }
        public int EvaluatedCharsCount { get; private set; }
        public int UniqueWeightCharsCount { get; private set; }
        

        public ASCIIWeightCalc(int fontsize, ASCIICategories categories)
        {
            _fontsize = fontsize;
            _textCoord = new Point(0, 0);
            _font = new Font(FontFamily.GenericMonospace, _fontsize);
            CharsWeighted = CalculateWeights(categories);
        }


        private List<char> CalculateWeights(ASCIICategories categories)
        {
            var dict = new Dictionary<char, double>();
            
            void addFromXtoY(int x, int y)
            {
                for (int i = x; i <= y; i++)
                {
                    char c = (char)i;
                    dict.Add(c, BlackWhiteValue(DrawChar(c)));
                }
            }

            if (categories.LowerCase) addFromXtoY(97, 122);
            if (categories.UpperCase) addFromXtoY(65, 90);
            if (categories.Numbers) addFromXtoY(48, 57);
            if (categories.SpecialChars1) addFromXtoY(32, 47);
            if (categories.SpecialChars2) addFromXtoY(58, 63);
            if (categories.SpecialChars3)
            {
                addFromXtoY(91, 95);
                addFromXtoY(123, 126);
            }
            if (categories.SpecialChars4) addFromXtoY(128, 254);


            dict = dict.OrderByDescending(keypair => keypair.Value)
                        .ToDictionary(keypair => keypair.Key, keypair => keypair.Value);
            
            List<double> werts = new List<double>();
            List<char> list = new List<char>();

            //kein foreach, weil sonst reihenfolge verloren gehen könnte
            for (int i = 0; i < dict.Count; i++)
            {
                if (!werts.Contains(dict.ElementAt(i).Value))
                {
                    werts.Add(dict.ElementAt(i).Value);
                    list.Add(dict.ElementAt(i).Key);
                }
            }
            EvaluatedCharsCount = dict.Count;
            UniqueWeightCharsCount = list.Count;
            return list;
        }

        private Bitmap DrawChar(char c)
        {
            Bitmap bmp = new Bitmap(_fontsize * 2, _fontsize * 2);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(255, 0, 0));
                g.DrawString(c.ToString(), _font, new SolidBrush(Color.FromArgb(0, 0, 255)), _textCoord);
                
                return bmp;
            }
        }

        private double BlackWhiteValue(Bitmap bmp)
        {
            double result = 0;

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    result += (c.B);
                }
            }
            result /= bmp.Height + bmp.Width;
            return result;
        }
    }
}