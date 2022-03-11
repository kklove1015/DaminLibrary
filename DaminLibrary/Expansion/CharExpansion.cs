using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class CharExpansion
    {
        public static bool IsWhiteSpace(this char word)
        {
            bool isWhiteSpace = char.IsWhiteSpace(word);
            return isWhiteSpace;
        }
        public static bool IsNumber(this char word)
        {
            bool isNumber = char.IsNumber(word);
            return isNumber;
        }
    }
}
