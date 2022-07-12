using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace ActiveRecordNET.Lib
{
    internal static class AdoConfigurationResolver
    {
        private static ConcurrentDictionary<Type, AdoConfigurationFactory> ConfigurationFactories;

        static AdoConfigurationResolver()
        {
            ConfigurationFactories = new ConcurrentDictionary<Type, AdoConfigurationFactory>();
        }

        public static AdoConfigurationFactory GetConfigurationFactory(Type type)
        {
            if(type is null)
                throw new ArgumentNullException(nameof(type));

            if(ConfigurationFactories.TryGetValue(type, out AdoConfigurationFactory value))
                return value;

            var resolvedAttribute = type.GetCustomAttribute<AdoConfigurationAttribute>();

            if (resolvedAttribute is null)
                throw new Exception($"{nameof(AdoConfigurationAttribute)} does't exists!");

            ConfigurationFactories.TryAdd(type, resolvedAttribute.Factory);

            return resolvedAttribute.Factory;
        }
    }
}
