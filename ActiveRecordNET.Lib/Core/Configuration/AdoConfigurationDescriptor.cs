using System;
using System.Reflection;

namespace ActiveRecordNET.Lib.Core
{
    public static class AdoConfigurationResolver
    {
        public static AdoConfigurationFactory GetConfigurationFactory(Type type)
        {
            if(type is null)
                throw new ArgumentNullException(nameof(type));

            var resolvedAttribute = type.GetCustomAttribute<AdoConfigurationAttribute>();

            if (resolvedAttribute is null)
                throw new Exception($"{nameof(AdoConfigurationAttribute)} does't exists!");

            return resolvedAttribute.Factory;
        }
    }
}
