using System;
using System.Collections.Generic;
using ARLiteNET.Lib.Integration.Tests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class ARLiteObjectTests
    {
        [TestMethod]
        public void Sample()
        {
            TestAdoObject adoObject = new TestAdoObject();

            IEnumerable<TestUserObject> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }

        [TestMethod]
        public void Sample2()
        {
            TestAdoObject adoObject = new TestAdoObject();

            adoObject.Add(new TestUserObject() { IsActive = true, Name = $"Test - {DateTime.Now}" });
        }
    }
}