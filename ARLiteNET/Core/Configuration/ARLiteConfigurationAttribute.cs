﻿using System;

namespace ARLiteNET
{
    [AttributeUsage(AttributeTargets.Class,
        Inherited = false, AllowMultiple = false)]
    public sealed class ARLiteConfigurationAttribute : Attribute
    {
        private readonly Type _configurationFactory;

        public ARLiteConfigurationAttribute(Type configurationFactory)
        {
            if (configurationFactory == null)
                throw new ArgumentNullException(nameof(configurationFactory));

            _configurationFactory = configurationFactory;
        }

        public ARLiteConfigurationFactory Factory => Activator.CreateInstance(_configurationFactory) as ARLiteConfigurationFactory;
    }
}
