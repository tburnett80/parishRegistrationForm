
using System;

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
    }
}
