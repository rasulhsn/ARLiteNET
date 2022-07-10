using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActiveRecordNET.Lib.Integration.Tests
{
    [TestClass]
    public class TestProxyTests
    {
        [TestMethod]
        public void Sample()
        {
            TestProxy adoProxy = new TestProxy();

            IEnumerable<TestObject> objects = adoProxy.GetAll();

            Assert.IsNotNull(objects);
        }
    }
}