using ARLiteNET.Lib.SQLite.Tests.Data.InMemory;
using ARLiteNET.Lib.Tests.Data.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteObjectTests
    {
        public SQLiteObjectTests() 
        {
            SQLiteInMemory.PrepareDB();
        }

        [TestMethod]
        public void GetAll_WhenCalled_ShouldReturnNonNullCollection()
        {
            TestSQLiteObject adoObject = new TestSQLiteObject();

            IEnumerable<TestUserObjectDto> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }
    }
}