using System;
using System.Linq;

namespace ParishForms.Common.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Will return the reflected type, and for generics generic type::arg type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string SimpleTypeOf(this Type type)
        {
            var typeString = type.ToString();
            if (!typeString.Contains('`') && !type.IsGenericType)
                return typeString;

            return $"{typeString.Substring(0, typeString.IndexOf('`'))}::{type.GetGenericArguments().FirstOrDefault()}";
        }
    }
}
