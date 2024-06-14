using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace ARLiteNET.Lib.SQLite
{
    internal static class SQLiteConfigurationResolver
    {
        private static ConcurrentDictionary<Type, SQLiteConfigurationFactory> ConfigurationFactories;

        static SQLiteConfigurationResolver()
        {
            ConfigurationFactories = new ConcurrentDictionary<Type, SQLiteConfigurationFactory>();
        }

        public static SQLiteConfigurationFactory GetConfigurationFactory(Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (ConfigurationFactories.TryGetValue(type, out SQLiteConfigurationFactory value))
                return value;

            var resolvedAttribute = type.GetCustomAttribute<SQLiteConfigurationAttribute>();

            if (resolvedAttribute is null)
                throw new Exception($"{nameof(SQLiteConfigurationAttribute)} does't exists!");

            ConfigurationFactories.TryAdd(type, resolvedAttribute.Factory);

            return resolvedAttribute.Factory;
        }
    }
}
