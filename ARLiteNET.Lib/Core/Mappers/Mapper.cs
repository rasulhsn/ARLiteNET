using System;
using System.Collections.Generic;
using System.Reflection;

namespace ARLiteNET.Lib.Core.Mappers
{
    internal static class Mapper
    {
        public static MapType Map<T>(T instance)
        {
            bool _IsPrimitive(PropertyInfo property)
            {
                Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) != null ?
                                    Nullable.GetUnderlyingType(property.PropertyType) : property.PropertyType;

                if (propertyType.IsPrimitive
                            || propertyType.Equals(typeof(decimal))
                            || propertyType.Equals(typeof(string))
                            || propertyType.Equals(typeof(DateTime))
                            || propertyType.Equals(typeof(bool)))
                {
                    return true;
                }

                return false;
            }

            Type instanceType = typeof(T);
            var properties = instanceType.GetProperties();

            List<MapMember> members = new List<MapMember>();

            foreach (var property in properties) {
                
                if(_IsPrimitive(property))
                {
                    members.Add(new MapMember(property.PropertyType,
                                                property.Name,
                                                property.GetValue(instance), true));
                }
            }

            return new MapType(instanceType, members);
        }
    }
}
