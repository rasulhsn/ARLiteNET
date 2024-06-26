using ARLiteNET.Lib.SQLite.Integration.Tests.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteObjectTests
    {
        [TestMethod]
        public void GetAll()
        {
            TestSQLiteObject adoObject = new TestSQLiteObject();

            IEnumerable<TestUserObjectDto> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }
    }
}