using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class DecimalExpansion
    {
        public static bool IsZeroOrUnsign(this decimal _decimal)
        {
            bool isZeroOrUnsign = _decimal <= 0;
            return isZeroOrUnsign;
        }
    }
}
