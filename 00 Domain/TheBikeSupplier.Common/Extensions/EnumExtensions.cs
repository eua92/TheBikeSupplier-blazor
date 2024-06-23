using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace TheBikeSupplier.Common.Extensions
{
    public static class EnumExtensions<T>
    {
        public static string GetDescription(T en)
        {
            var attribute = GetDisplayAttribute(en);
            return GetDescriptionBase(en, attribute?.GetName());
        }

        public static string GetDescriptionShort(T en)
        {
            var attribute = GetDisplayAttribute(en);
            return GetDescriptionBase(en, attribute?.GetShortName());
        }

        private static string GetDescriptionBase(T en, string name)
        {
            if (name != null)
                return name;

            return en.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute(T en)
        {
            Type type = en.GetType();
            MemberInfo[] memberInfo = type.GetMember(en.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().ToList();
                if ((attributes != null && attributes.Count() > 0)) return attributes[0];
            }
            return null;
        }
    }
}
