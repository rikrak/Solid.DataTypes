using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.DataTypes.Infrastructure;

namespace Solid.DataTypes
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Adapted from: http://tmont.com/blargh/2011/3/determining-if-an-open-generic-type-isassignablefrom-a-type
    /// </remarks>
    public static class TypeExtensions
    {
        #region IsAssignableToGenericType
        /// <summary>
        /// Determines whether the <paramref name="genericType"/> is assignable from
        /// <paramref name="givenType"/> taking into account generic definitions
        /// </summary>
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType == null || genericType == null)
            {
                return false;
            }

            return givenType == genericType
                   || givenType.MapsToGenericTypeDefinition(genericType)
                   || givenType.HasInterfaceThatMapsToGenericTypeDefinition(genericType)
                   || givenType.BaseType.IsAssignableToGenericType(genericType);
        }

        private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return givenType
                .GetInterfaces()
                .Where(it => it.IsGenericType)
                .Any(it => it.GetGenericTypeDefinition() == genericType);
        }

        private static bool MapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return genericType.IsGenericTypeDefinition
                   && givenType.IsGenericType
                   && givenType.GetGenericTypeDefinition() == genericType;
        }
        #endregion

        /// <summary>
        /// Determines if the given type can be used in a foreach statement
        /// </summary>
        public static bool IsEnumerable(this Type theType)
        {
            if(theType.IsNull()){throw new ArgumentNullException(nameof(theType));}

            if (theType.IsArray) return true;

            var enumerableType = typeof(System.Collections.IEnumerable);

            return enumerableType.IsAssignableFrom(theType);
        }

        /// <summary>
        /// Gets the element type for an enumerable type
        /// </summary>
        /// <remarks>
        /// for example for IEnumerable{int} it will return int
        /// </remarks>
        /// <param name="enumeratedType">An enumerable type</param>
        /// <returns></returns>
        public static Type GetEnumerationType(this Type enumeratedType)
        {
            if (enumeratedType.IsNull()){throw new ArgumentNullException(nameof(enumeratedType));}

            if (!enumeratedType.IsEnumerable())
            {
                var message = $"{enumeratedType.Name} cannot be enumerated";
                throw new InvalidOperationException(message);
            }

            if (enumeratedType.IsArray) return enumeratedType.GetElementType();

            if (enumeratedType.MapsToGenericTypeDefinition(typeof(IEnumerable<>)))
            {
                return enumeratedType.GetGenericArguments()[0];
            }
            if (enumeratedType.IsAssignableToGenericType(typeof(IEnumerable<>)))
            {
                var enumerableInterface = enumeratedType.GetInterface(typeof(IEnumerable<>).Name);
                return enumerableInterface.GetGenericArguments()[0];
            }

            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(enumeratedType))
            {
                return typeof(object);
            }

            throw new InvalidOperationException($"{enumeratedType.Name} cannot be enumerated");
        }

        /// <summary>
        /// Determines if the given type is decorated with the given attribute
        /// </summary>
        /// <typeparam name="TAttribute">The attribute of interest</typeparam>
        /// <param name="subject">The item to check</param>
        /// <returns>True if the type has the requested attribute</returns>
        public static bool HasAttribute<TAttribute>(this Type subject)
            where TAttribute : Attribute
        {
            return subject.HasAttribute(typeof(TAttribute));
        }

        /// <summary>
        /// Determines if the given type is decorated with the given attribute
        /// </summary>
        /// <param name="subject">The item to check</param>
        /// <param name="attributeType">The attribute of interest</param>
        /// <returns>True if the type has the requested attribute</returns>
        public static bool HasAttribute(this Type subject, Type attributeType)
        {
            return Attribute.IsDefined(subject, attributeType);
        }

        /// <summary>
        /// Determines if the given Member is decorated with the given attribute
        /// </summary>
        /// <typeparam name="TAttribute">The attribute of interest</typeparam>
        /// <param name="subject">The item to check</param>
        /// <returns>True if the member has the requested attribute</returns>
        public static bool HasAttribute<TAttribute>(this MemberInfo subject)
            where TAttribute : Attribute
        {
            return Attribute.IsDefined(subject, typeof(TAttribute));
        }

        /// <summary>
        /// Fetches the given <typeparamref name="TAttribute"/> from the given <paramref name="subject"/>
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="subject"></param>
        /// <returns>The attribute, or null if one is not defined</returns>
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo subject)
            where TAttribute : Attribute
        {
            if (subject.IsNull()){throw new ArgumentNullException(nameof(subject));}

            return Attribute.GetCustomAttribute(subject, typeof(TAttribute)) as TAttribute;
        }

        public static Type GetReturnType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new ArgumentException
                    (
                        "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }
    }
}