using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class ICollectionExpansion
    {
        public static bool IsNullOrEmpty(this ICollection iCollection)
        {
            if (iCollection.IsNull() == true) { return true; }
            if (iCollection.Count <= 0) { return true; }
            return false;
        }
    }
}
