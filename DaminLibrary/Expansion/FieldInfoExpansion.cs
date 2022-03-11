using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class FieldInfoExpansion
    {
        public static Type FindType(this FieldInfo fieldInfo)
        {
            switch (fieldInfo.FieldType.Name)
            {
                case "String":
                    return typeof(string);
                case "Double":
                    return typeof(double);
                case "Decimal":
                    return typeof(decimal);
                default:
                    return null;
            }
        }
    }
}
