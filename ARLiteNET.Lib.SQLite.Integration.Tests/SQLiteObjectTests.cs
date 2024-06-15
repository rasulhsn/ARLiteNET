using ARLiteNET.Lib.SQLite.Integration.Tests.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteObjectTests
    {
        [TestMethod]
        public void GetAllObjects()
        {
            TestSQLiteObject adoObject = new TestSQLiteObject();

            IEnumerable<TestUserObjectDto> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }

        [TestMethod]
        public void AddObject()
        {
            TestSQLiteObject adoObject = new TestSQLiteObject();

            adoObject.Add(new TestUserObjectDto() { IsActive = true, Name = $"Test - {DateTime.Now}" });
        }
    }
}