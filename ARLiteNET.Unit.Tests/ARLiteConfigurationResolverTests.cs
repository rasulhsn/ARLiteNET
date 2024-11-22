using System;
using ARLiteNET.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARLiteNET.Lib.Unit.Tests
{
    [TestClass]
    public class ARLiteConfigurationResolverTests
    {
        [TestMethod]
        public void ResolveConfigurationFactory_WhenGivenNull_ShouldThrowArgumentNullException() 
            => Assert.ThrowsException<ArgumentNullException>(() => ARLiteConfigurationResolver.ResolveConfigurationFactory(null));

        [TestMethod]
        public void ResolveConfigurationFactory_WhenGivenInvalidType_ShouldThrowException() 
            => Assert.ThrowsException<Exception>(() => ARLiteConfigurationResolver.ResolveConfigurationFactory(typeof(ARLiteConfigurationResolverTests)));
    }
}