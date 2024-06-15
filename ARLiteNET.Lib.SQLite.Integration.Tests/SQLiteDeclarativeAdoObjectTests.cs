using ARLiteNET.Lib.Integration.Tests.Helper;

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