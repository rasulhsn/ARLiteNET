using System;
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

        [TestMethod]
        public void Sample2()
        {
            TestProxy adoProxy = new TestProxy();

            adoProxy.Add(new TestObject() { IsActive = true, Name = $"Test - {DateTime.Now}" });
        }
    }
}