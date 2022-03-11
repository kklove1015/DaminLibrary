using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class PropertyInfoExpansion
    {
        public static Type FindType(this PropertyInfo propertyInfo)
        {
            switch (propertyInfo.PropertyType.Name)
            {
                case "String":
                    return typeof(string);
                case "Double":
                    return typeof(double);
                case "Decimal":
                    return typeof(decimal);
                case "Int32":
                    return typeof(int);
                case "Boolean":
                    return typeof(bool);

                default:
                    return null;
            }
        }
    }
}
