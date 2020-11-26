using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Solid.DataTypes
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns a user-friendly value for the enum
        /// </summary>
        /// <remarks>
        /// Uses the value from a <see cref="DescriptionAttribute"/> if there is one,
        /// otherwise uses the text value of the enum.
        /// </remarks>
        /// <param name="value">The enum</param>
        /// <returns>A string representation of the <paramref name="value"/></returns>
        public static string ToDescription(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();

            var displayAttribute = value.GetAttribute<DisplayAttribute>();

            if (attribute != null)
            {
                return attribute.Description;
            }

            return displayAttribute != null ? displayAttribute.GetName() : value.ToString();
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var attributeValue = value.GetType().GetField(value.ToString());
            return attributeValue.GetAttribute<TAttribute>();
        }

        /// <summary>
        /// Creates a dictionary from an enumeration, keyed on the integer value of the enumeration.  The dictionary values are a string representation of the enum.
        /// </summary>
        /// <typeparam name="T">The enumeration</typeparam>
        /// <returns>the Dictionary</returns>
        public static IDictionary<int, string> ToIntDictionary<T>() where T : Enum
        {
            return ToIntDictionary<T>(t => true);
        }

        /// <summary>
        /// Creates a dictionary from an enumeration, keyed on the integer value of the enumeration.  The dictionary values are a string representation of the enum.
        /// </summary>
        /// <param name="valueFilter">Allows the results to be filtered</param>
        /// <typeparam name="T">The enumeration</typeparam>
        /// <returns>the Dictionary</returns>
        public static IDictionary<int, string> ToIntDictionary<T>(Func<T, bool> valueFilter) where T : Enum
        {
            valueFilter ??= x => true; //select all if no filter is specified

            var accrualPeriodOptions = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Where(valueFilter);
            var result = accrualPeriodOptions.ToDictionary(ToInt, e => e.ToDescription());

            return result;
        }

        /// <summary>
        /// Gets the enum values as a list
        /// </summary>
        /// <typeparam name="T">The enumeration</typeparam>
        /// <returns>an enumeration</returns>
        public static IEnumerable<T> GetValues<T>() where T : Enum
        {
            return GetValues<T>((x) => true);
        }

        /// <summary>
        /// Gets the enum values as a list
        /// </summary>
        /// <param name="valueFilter">Allows the results to be filtered</param>
        /// <typeparam name="T">The enumeration</typeparam>
        /// <returns>an enumeration</returns>
        public static IEnumerable<T> GetValues<T>(Func<T, bool> valueFilter) where T : Enum
        {
            valueFilter ??= x => true; //select all if no filter is specified

            var values = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Where(valueFilter);
            return values;
        }

        /// <summary>
        /// Converts an enumeration value to an int
        /// </summary>
        /// <typeparam name="T">The Enum type</typeparam>
        /// <param name="value">The value to convert</param>
        /// <exception cref="InvalidOperationException">thrown if the enum is not based on integers</exception>
        /// <returns>
        /// The underlying integer value
        /// </returns>
        public static int ToInt<T>(this T value) where T : Enum
        {
            if (Enum.GetUnderlyingType(typeof(T)) == typeof(int))
            {
                return Convert.ToInt32(value);
            }
            throw new InvalidOperationException($"the enum {typeof(T)} is not based on an integer");
        }

        /// <summary>
        /// Returns the specified elements of a bitwise enum flag value
        /// </summary>
        /// <remarks>Adapted from https://stackoverflow.com/a/42008114/90609</remarks>
        /// <typeparam name="T">the enum type</typeparam>
        /// <param name="flags">The value</param>
        /// <returns>An array of the individually specified flags</returns>
        public static IEnumerable<T> GetUniqueFlags<T>(this T flags) where T : Enum
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("The generic type parameter must be an Enum.");
            }

            if (flags.GetType() != typeof(T))
            {
                throw new ArgumentException("The generic type parameter does not match the target type.");
            }

            ulong flag = 1;
            foreach (var value in Enum.GetValues(flags.GetType()).Cast<T>())
            {
                ulong bits = Convert.ToUInt64(value);
                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits && flags.HasFlag((T)value))
                {
                    yield return value;
                }
            }
        }
    }
}
