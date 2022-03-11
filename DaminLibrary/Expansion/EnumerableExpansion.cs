using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class EnumerableExpansion
    {
        public static List<T> CastAdvance<T>(this IEnumerable iEnumerable)
        {
            if (iEnumerable.IsNull() == true) { return null; }
            IEnumerable<T> castTIEnumerable = iEnumerable.Cast<T>();
            List<T> castTList = castTIEnumerable.ToList();
            return castTList;
        }
    }
}
