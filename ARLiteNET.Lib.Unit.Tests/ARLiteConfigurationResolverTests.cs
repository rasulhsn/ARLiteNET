using System;
using ARLiteNET.Lib.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARLiteNET.Lib.Unit.Tests
{
    [TestClass]
    public class ARLiteConfigurationResolverTests
    {
        [TestMethod]
        public void GetConfigurationFactory_WhenGivenNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ARLiteConfigurationResolver.GetConfigurationFactory(null));
        }

        [TestMethod]
        public void GetConfigurationFactory_WhenGivenInvalidType_ShouldThrowException()
        {
            Assert.ThrowsException<Exception>(() => ARLiteConfigurationResolver.GetConfigurationFactory(typeof(ARLiteConfigurationResolverTests)));
        }
    }
}