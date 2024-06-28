using System;
using System.Collections.Generic;
using System.Reflection;

namespace ARLiteNET.Lib.Core.Mappers
{
    internal static class Mapper
    {
        public static MapType Map<T>(T instance)
        {
            bool _TypeIsPrimitive(Type propertyType)
            {
                if (propertyType.IsPrimitive
                            || propertyType.Equals(typeof(float))
                            || propertyType.Equals(typeof(decimal))
                            || propertyType.Equals(typeof(double))
                            || propertyType.Equals(typeof(string))
                            || propertyType.Equals(typeof(DateTime))
                            || propertyType.Equals(typeof(bool)))
                {
                    return true;
                }

                return false;
            }

            Type instanceType = typeof(T);
            var properties = instanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            List<MapMember> members = new List<MapMember>();

            foreach (var property in properties)
            {
                Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) != null ?
                                    Nullable.GetUnderlyingType(property.PropertyType) : property.PropertyType;

                if (_TypeIsPrimitive(propertyType))
                {
                    members.Add(new MapMember(propertyType,
                                                property.Name,
                                                property.GetValue(instance), true));
                }
                else
                {
                    members.Add(new MapMember(propertyType,
                                                property.Name,
                                                property.GetValue(instance), false));
                }
            }

            return new MapType(instanceType, members);
        }
    }
}
