using DaminLibrary.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class TypeExpansion
    {
        public static object GetValue(this Type type, string name, object obj)
        {
            PropertyInfo getProperty = type.GetProperty(name);
            object getValue = getProperty.GetValue(obj);
            return getValue;
        }
        public static string GetValueToString(this Type type, string name, object obj)
        {
            object getValue = type.GetValue(name, obj);
            string getValueToString = getValue.ToString();
            return getValueToString;
        }
        public static T GetValue<T>(this Type type, string name, object obj)
        {
            string getValueToString = type.GetValueToString(name, obj);
            T getValueToStringToEnum = getValueToString.ToEnum<T>();
            return getValueToStringToEnum;
        }
        public static List<Type> GetInterfaceTypeList(this Type type)
        {
            Type[] getInterfaces = type.GetInterfaces();
            List<Type> getInterfaceTypeList = getInterfaces.ToList();
            return getInterfaceTypeList;
        }
        public static Type GetInterfaceType(this Type type)
        {
            List<Type> getInterfaceTypeList = type.GetInterfaceTypeList();
            Type firstInterfaceType = getInterfaceTypeList.First();
            return firstInterfaceType;
        }
        public static string CreateClassName(this Type type, string separator = "_")
        {
            if (type.IsGenericType == true)
            {
                Type[] getGenericArguments = type.GetGenericArguments();
                type = getGenericArguments.First();
            }
            List<string> splitTypeFullNameItemList = type.FullName.Split(".");
            string lastTypeFullNameItemList = splitTypeFullNameItemList.Last();
            List<string> splitLastTypeFullNameItemList = lastTypeFullNameItemList.Split("+");
            string joinSplitLastTypeFullNameItemList = splitLastTypeFullNameItemList.Join(separator);
            return joinSplitLastTypeFullNameItemList;
        }
        public static string CreateFileName(this Type type)
        {
            string getDate = DateTime.Now.GetDate();
            string createClassName = type.CreateClassName(".");
            var fileNameItemList = new List<string>() { "[" + getDate + "]", "[" + createClassName + "]" };
            string fileName = fileNameItemList.Join();
            return fileName;
        }
        public static List<MemberInfo> FindAllMemberInfoList(this Type type, MemberTypes memberTypes)
        {
            MemberInfo[] getMembers = type.GetMembers();
            List<MemberInfo> getMemberInfoList = getMembers.ToList();
            List<MemberInfo> findAllMemberInfoList = getMemberInfoList.FindAll(x => x.MemberType == memberTypes);
            return findAllMemberInfoList;
        }
        public static List<Property> GetPropertyList(this Type type, bool isChildType = true, Names names = null)
        {
            if (type.IsGenericType == true) { return null; }
            List<MemberInfo> findAllMemberInfoList = type.FindAllMemberInfoList(MemberTypes.Property);
            var propertyList = new List<Property>();
            try
            {
                for (int i = 0; i <= findAllMemberInfoList.Count - 1; i++)
                {
                    try
                    {
                        var originalNameItemList = new List<string>();
                        var translationNameItemList = new List<string>();
                        if (names.IsNull() != true)
                        {
                            originalNameItemList.Add(names.originalName);
                            if (names.translationName.IsNullOrWhiteSpace() != true)
                            {
                                translationNameItemList.Add(names.translationName);
                            }
                        }
                        PropertyInfo getProperty = type.GetProperty(findAllMemberInfoList[i].Name);
                        if (getProperty.IsNull() == true) { continue; }
                        Attribute attribute = getProperty.GetCustomAttribute(typeof(DataMemberAttribute));
                        if ((attribute is DataMemberAttribute) == false) { continue; }


                        originalNameItemList.Add(getProperty.Name);

                        CustomAttributeData firstCustomAttributeData = getProperty.CustomAttributes.First();
                        if (firstCustomAttributeData.IsNull() == true) { continue; }

                        CustomAttributeNamedArgument firstCustomAttributeNamedArgument = firstCustomAttributeData.NamedArguments.First();
                        if (firstCustomAttributeNamedArgument.IsNull() == true) { continue; }

                        CustomAttributeTypedArgument customAttributeTypedArgument = firstCustomAttributeNamedArgument.TypedValue;
                        if (customAttributeTypedArgument.IsNull() == true) { continue; }

                        string customAttributeTypedArgumentString = customAttributeTypedArgument.Value.ToString();
                        if (customAttributeTypedArgumentString.IsNullOrWhiteSpace() != true)
                        {
                            translationNameItemList.Add(customAttributeTypedArgumentString);
                        }
                        string originalName = originalNameItemList.Join(".");
                        if (originalName.IsNullOrWhiteSpace() == true) { continue; }
                        string translationName = translationNameItemList.Join(" ");
                        if (translationName.IsNullOrWhiteSpace() == true) { continue; }
                        var propertyNames = new Names(originalName, translationName);
                        if (propertyNames.IsNull() == true) { continue; }
                        Type findType = getProperty.FindType();
                        switch (findType.IsNull() != true)
                        {
                            case true:
                                var property = new Property(propertyNames, findType);

                                var asd = property.names.translationName;
                                propertyList.Add(property);
                                break;
                            case false:
                                if (isChildType == true)
                                {
                                    var getPropertyList = getProperty.PropertyType.GetPropertyList(isChildType, propertyNames);
                                    if (getPropertyList.IsNullOrEmpty() == true) { continue; }
                                    propertyList.AddRange(getPropertyList);

                                }
                                break;
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return propertyList;
        }
        public static object GetPropertyValue(this Type type, string name, object obj)
        {
            List<string> nameItemList = name.Split(".");
            object propertyValue = obj;
            for (int i = 0; i <= nameItemList.Count - 1; i++)
            {
                if (propertyValue.IsNull() == true) { break; }

                Type getType = propertyValue.GetType();
                PropertyInfo getProperty = getType.GetProperty(nameItemList[i]);
                propertyValue = getProperty.GetValue(propertyValue);
            }
            return propertyValue;
        }
    }
}
