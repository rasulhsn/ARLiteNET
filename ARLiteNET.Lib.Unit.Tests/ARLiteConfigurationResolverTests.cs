using System;
using ARLiteNET.Lib.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARLiteNET.Lib.Unit.Tests
{
    [TestClass]
    public class ARLiteConfigurationResolverTests
    {
        [TestMethod]
        public void GetConfigurationFactory_InvalidType_ThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ARLiteConfigurationResolver.GetConfigurationFactory(null));
        }

        [TestMethod]
        public void GetConfigurationFactory_UndeclaredType_ThrowException()
        {
            Assert.ThrowsException<Exception>(() => ARLiteConfigurationResolver.GetConfigurationFactory(typeof(ARLiteConfigurationResolverTests)));
        }
    }
}