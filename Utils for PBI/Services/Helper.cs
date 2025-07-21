using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Utils_for_PBI.Services
{
    // Source: https://www.c-sharpcorner.com/article/creating-an-extension-method-to-get-enum-description/
    public static class Extensions
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            else
            {
                return enumValue.ToString();
            }
        }
    }
}
