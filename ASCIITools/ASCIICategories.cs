using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIITools
{
    public struct ASCIICategories
    {
        public bool LowerCase { get; set; }
        public bool UpperCase { get; set; }
        public bool Numbers { get; set; }
        public bool SpecialChars1 { get; set; }
        public bool SpecialChars2 { get; set; }
        public bool SpecialChars3 { get; set; }
        public bool SpecialChars4 { get; set; }

        public bool ContainsAtLeastOne()
        {
            return LowerCase || UpperCase || Numbers || SpecialChars1 || SpecialChars2 || SpecialChars3 || SpecialChars4;
        }
    }
}
