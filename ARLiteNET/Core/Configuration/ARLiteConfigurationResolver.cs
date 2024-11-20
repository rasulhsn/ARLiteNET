using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace ARLiteNET.Core
{
    internal static class ARLiteConfigurationResolver
    {
        private static readonly ConcurrentDictionary<Type, ARLiteConfigurationFactory> _configurationFactories;

        static ARLiteConfigurationResolver()
        {
            _configurationFactories = new ConcurrentDictionary<Type, ARLiteConfigurationFactory>();
        }

        public static ARLiteConfigurationFactory ResolveConfigurationFactory(Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (_configurationFactories.TryGetValue(type, out ARLiteConfigurationFactory value))
                return value;

            var resolvedAttribute = type.GetCustomAttribute<ARLiteConfigurationAttribute>();

            if (resolvedAttribute is null)
                throw new Exception($"{nameof(ARLiteConfigurationAttribute)} does not exists!");

            _configurationFactories.TryAdd(type, resolvedAttribute.Factory);

            return resolvedAttribute.Factory;
        }

        public static bool TryResolveConfigurationFactory(Type type, out ARLiteConfigurationFactory resolvedFactory)
        {
            try
            {
                var factory = ResolveConfigurationFactory(type);
                resolvedFactory = factory;

                return true;
            }
            catch
            {
                resolvedFactory = null;
                return false;
            }
        }
    }
}
