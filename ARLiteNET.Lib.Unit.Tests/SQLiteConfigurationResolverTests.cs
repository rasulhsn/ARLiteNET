using System;
using ARLiteNET.Lib.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARLiteNET.Lib.Unit.Tests
{
    [TestClass]
    public class SQLiteConfigurationResolverTests
    {
        [TestMethod]
        public void GetConfigurationFactory_InvalidType_ThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SQLiteConfigurationResolver.GetConfigurationFactory(null));
        }

        [TestMethod]
        public void GetConfigurationFactory_UndeclaredType_ThrowException()
        {
            Assert.ThrowsException<Exception>(() => SQLiteConfigurationResolver.GetConfigurationFactory(typeof(SQLiteConfigurationResolverTests)));
        }
    }
}