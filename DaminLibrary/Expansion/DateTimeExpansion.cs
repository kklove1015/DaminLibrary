using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class DateTimeExpansion
    {
        public static string GetDateTimeNow(this DateTime dateTime)
        {
            string dateTimeString = dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
            return dateTimeString;
        }
        public static string GetDate(this DateTime dateTime)
        {
            string dateTimeString = dateTime.ToString("yyyy-MM-dd");
            return dateTimeString;
        }
        public static string GetTime(this DateTime dateTime)
        {
            string dateTimeString = dateTime.ToString("HH:mm:ss");
            return dateTimeString;
        }
        public static long GetNowTimeStamp()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
    }
}
