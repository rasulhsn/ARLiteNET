using ARLiteNET.Lib.Integration.Tests.Helper;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteObjectTests
    {
        [TestMethod]
        public void GetAllObjects()
        {
            TestSQLiteObject adoObject = new TestSQLiteObject();

            IEnumerable<TestUserObject> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }

        [TestMethod]
        public void AddObject()
        {
            TestSQLiteObject adoObject = new TestSQLiteObject();

            adoObject.Add(new TestUserObject() { IsActive = true, Name = $"Test - {DateTime.Now}" });
        }
    }
}