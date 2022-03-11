using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class ListExpansion
    {
        #region Join
        public static string Join(this List<string> textList, string separator = "") 
        {
            string join = string.Join(separator, textList);
            return join;
        }
        public static string Join(this List<char> wordList, string separator = "")
        {
            string join = string.Join(separator, wordList);
            return join;
        }
        #endregion

        #region ETC
        public static List<List<T>> Division<T>(this List<T> tList, int denominator, CancellationToken cancellationToken = default)
        {
            int division = tList.Count.Division(denominator);
            var tListList = new List<List<T>>();
            for (int i = 1; i <= division; i++)
            {
                if (cancellationToken.IsCancellationRequested == true) { break; }

                int index = (i - 1) * denominator;
                int count = denominator;
                int remainder = tList.Count - index;
                if (remainder < denominator) { count = remainder; }
                List<T> getRange = tList.GetRange(index, count);
                tListList.Add(getRange);
            }
            return tListList;
        }
        public static List<TResult> SelectAdvance<TSource, TResult>(this List<TSource> source, Func<TSource, TResult> selector)
        {
            IEnumerable<TResult> select = source.Select(selector);
            List<TResult> selectList = select.ToList();
            IEnumerable<TResult> distinct = selectList.Distinct();
            List<TResult> distinctList = distinct.ToList();
            return distinctList;
        }
        #endregion
    }
}
