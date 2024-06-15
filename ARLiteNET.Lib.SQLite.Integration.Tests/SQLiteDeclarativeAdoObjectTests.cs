using System.Collections.Generic;
using ARLiteNET.Lib.Integration.Tests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteDeclarativeAdoObjectTests
    {
        [TestMethod]
        public void Sample()
        {
            TestSQLiteDeclarativeAdoObject adoObject = new TestSQLiteDeclarativeAdoObject();

            IEnumerable<TestUserObject> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }
    }
}