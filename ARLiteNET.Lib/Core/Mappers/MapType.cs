using System;
using System.Collections.Generic;
using System.Linq;

namespace ARLiteNET.Lib.Core.Mappers
{
    public sealed class MapType
    {
        public Type RootType { get; }
        public IEnumerable<MapMember> Members { get; }
        public bool HasMembers => Members != null && Members.Any();

        public MapType(Type rootType, IEnumerable<MapMember> members)
        {
            RootType = rootType;
            Members = members;
        }
    }

    public sealed class MapMember
    {
        public Type Type { get; }
        public string Name { get; }
        public object Value { get; }
        public bool IsPrimitive { get; }

        public MapMember(Type type, string name, object value, bool isPrimitive)
        {
            Type = type;
            Name = name;
            Value = value;
            IsPrimitive = isPrimitive;
        }
    }
}
