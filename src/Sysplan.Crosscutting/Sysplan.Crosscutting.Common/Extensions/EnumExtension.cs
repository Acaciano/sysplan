using System;
using System.ComponentModel;
using System.Linq;

namespace Sysplan.Crosscutting.Common.Extensions
{
    public static class EnumExtension
    {
        public static string GetResourceDescription(this Enum @enum)
        {
            if (@enum == null)
                return string.Empty;

            var description = @enum.ToString();
            var fieldInfo = @enum.GetType().GetField(description);
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Any() ? attributes[0].Description : description;
        }

        public static string GetDescription(this Enum @enum)
        {
            var enumeratorItems = @enum.ToString().Split(',');
            var description = new string[enumeratorItems.Length];

            for (var i = 0; i < enumeratorItems.Length; i++)
            {
                var fieldInfo = @enum.GetType().GetField(enumeratorItems[i]?.Trim());
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                description[i] = (attributes.Length > 0) ? attributes[0].Description : enumeratorItems[i]?.Trim();
            }

            return string.Join(", ", description);
        }

        public static T GetEnumerator<T>(string description)
        {
            if (description == null)
                return default(T);

            description = description.ToUpper();

            var type = typeof(T);

            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute != null)
                {
                    if (attribute.Description.ToUpper() == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.ToUpper() == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new InvalidCastException(string.Format("Enumerator {0} with description {1} not found", type, description));
        }

        public static T GetEnumeratorByName<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
                return default(T);

            name = name.ToUpper();

            var type = typeof(T);

            if (!type.IsEnum)
                throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                if (field.Name.ToUpper() == name)
                    return (T)field.GetValue(null);
            }

            throw new InvalidCastException(string.Format("Enumerator {0} with name {1} not found", type, name));
        }

        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type)
                .Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                .Select(d => d)
                .FirstOrDefault();

            if (name == null)
                return string.Empty;

            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }
}