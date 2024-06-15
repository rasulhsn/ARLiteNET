using System;
using System.Collections.Generic;
using System.Linq;

namespace ARLiteNET.Lib.Core.Mappers
{
    public sealed class MapType
    {
        public Type Info { get; }
        public string Name { get; }
        public object Value { get; }
        public bool IsPrimitive { get; }
        public IEnumerable<MapType> NestedTypes { get; }
        public bool HasNestedTypes => NestedTypes != null && NestedTypes.Any();
    }
}
