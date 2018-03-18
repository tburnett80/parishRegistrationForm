
using System;
using System.Linq;

namespace ParishForms.Common.Extensions
{
    /// <summary>
    /// Common string extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Null safe trim operation
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TryTrim(this string value)
        {
            return string.IsNullOrEmpty(value)
                ? value
                : value.Trim();
        }

        public static string GuidToId(this Guid value)
        {
            return value.ToString().Replace("-", "");
        }

        public static int TryToInt(this string value)
        {
            return string.IsNullOrEmpty(value.TryTrim())
                ? 0
                : int.Parse(value.TryTrim());
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value.TryTrim());
        }

        public static bool IsNumeric(this string value)
        {
            return value.HasValue() && value.All(char.IsDigit);
        }

        public static string TryToTrimedUpper(this string value)
        {
            return value.HasValue()
                ? value.TryTrim().ToUpper()
                : value;
        }

        public static string TryTrimRemove(this string value, string toRemove)
        {
            return value.HasValue()
                ? value.Replace(toRemove, "").TryTrim()
                : value;
        }
    }
}
