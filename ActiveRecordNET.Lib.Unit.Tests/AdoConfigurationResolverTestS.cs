using System;
using ActiveRecordNET.Lib.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActiveRecordNET.Lib.Unit.Tests
{
    [TestClass]
    public class AdoConfigurationResolverTests
    {
        [TestMethod]
        public void GetConfigurationFactory_InvalidType_ThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => AdoConfigurationResolver.GetConfigurationFactory(null));
        }

        [TestMethod]
        public void GetConfigurationFactory_UndeclaredType_ThrowException()
        {
            Assert.ThrowsException<Exception>(() => AdoConfigurationResolver.GetConfigurationFactory(typeof(AdoConfigurationResolverTests)));
        }
    }
}