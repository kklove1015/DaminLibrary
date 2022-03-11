using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class ObjectExpansion
    {
        #region Null Check
        public static bool IsNull(this object value) 
        {
            bool isNull = value is null;
            return isNull;
        }
        #endregion

        #region Json
        private static string SerializeObject(this object value) 
        {
            string serializeObject = JsonConvert.SerializeObject(value);
            return serializeObject; ;
        }
        public static string ToJson(this object value) 
        {
            string serializeObject = value.SerializeObject();
            if (serializeObject == "null") { return null; }
            return serializeObject;
        }
        #endregion

        #region Split
        public static List<string> Split(this object _object, string separator) 
        {
            string _string = _object.ToString();
            List<string> splitStringList = _string.Split(separator);
            return splitStringList;
        }
        #endregion

        #region Join
        public static string Join(this object[] objectArray, string separator)
        {
            var textList = new List<string>();
            for (int i = 0; i <= objectArray.Length - 1; i++)
            {
                string objectString = objectArray[i].ToString();
                textList.Add(objectString);
            }
            string join = textList.Join(separator);
            return join;
        }
        #endregion

        #region Convert
        public static decimal ToDecimal(this object value)
        {
            var valueToDecimal = Convert.ToDecimal(value);
            return valueToDecimal;
        }
        public static Int32 ToInt32(this object value)
        {
            Int32 int32 = Convert.ToInt32(value);
            return int32;
        }
        public static Int32 ToInt32(this object value, int digits)
        {
            Int32 int32 = value.ToInt32();
            int pow = 10.Pow(digits);
            int32 = ((int32 + (pow - 1)) / pow) * pow;
            return int32;
        }
        #endregion

        #region ETC
        public static int Division(this object value, decimal denominator)
        {
            decimal valueToDecimal = value.ToDecimal();
            decimal ceiling = Math.Ceiling(valueToDecimal / denominator);
            int ceilingToInt32 = ceiling.ToInt32();
            return ceilingToInt32;
        }
        #endregion
    }
}
