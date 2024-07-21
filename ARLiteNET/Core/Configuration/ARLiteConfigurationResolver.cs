using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace ARLiteNET.Core
{
    internal static class ARLiteConfigurationResolver
    {
        private static ConcurrentDictionary<Type, ARLiteConfigurationFactory> ConfigurationFactories;

        static ARLiteConfigurationResolver()
        {
            ConfigurationFactories = new ConcurrentDictionary<Type, ARLiteConfigurationFactory>();
        }

        public static ARLiteConfigurationFactory GetConfigurationFactory(Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (ConfigurationFactories.TryGetValue(type, out ARLiteConfigurationFactory value))
                return value;

            var resolvedAttribute = type.GetCustomAttribute<ARLiteConfigurationAttribute>();

            if (resolvedAttribute is null)
                throw new Exception($"{nameof(ARLiteConfigurationAttribute)} does't exists!");

            ConfigurationFactories.TryAdd(type, resolvedAttribute.Factory);

            return resolvedAttribute.Factory;
        }
    }
}
