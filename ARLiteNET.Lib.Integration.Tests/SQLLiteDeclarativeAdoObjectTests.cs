using System.Collections.Generic;
using ARLiteNET.Lib.Integration.Tests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLLiteDeclarativeAdoObjectTests
    {
        [TestMethod]
        public void Sample()
        {
            TestSQLLiteDeclarativeAdoObject adoObject = new TestSQLLiteDeclarativeAdoObject();

            IEnumerable<TestUserObject> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }
    }
}