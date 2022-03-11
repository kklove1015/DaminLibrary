using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class Int32Expansion
    {
        public static bool IsZeroOrUnsign(this Int32 int32)
        {
            bool isZeroOrUnsign = int32 <= 0;
            return isZeroOrUnsign;
        }
        public static int Pow(this Int32 x, double y)
        {
            double pow = Math.Pow(x, y);
            Int32 int32 = pow.ToInt32();
            return int32;
        }
    }
}
