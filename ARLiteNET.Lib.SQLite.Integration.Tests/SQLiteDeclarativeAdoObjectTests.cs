using ARLiteNET.Lib.SQLite.Integration.Tests.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteDeclarativeAdoObjectTests
    {
        [TestMethod]
        public void Sample()
        {
            TestSQLiteDeclarativeAdoObject adoObject = new TestSQLiteDeclarativeAdoObject();

            IEnumerable<TestUserObjectDto> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }
    }
}