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
        public void GetAllObjects()
        {
            TestARLiteObject adoObject = new TestARLiteObject();

            IEnumerable<TestUserObject> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }

        [TestMethod]
        public void AddObject()
        {
            TestARLiteObject adoObject = new TestARLiteObject();

            adoObject.Add(new TestUserObject() { IsActive = true, Name = $"Test - {DateTime.Now}" });
        }
    }
}