using ARLiteNET.Lib.Tests.Data.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteDeclarativeAdoObjectTests
    {
        [TestMethod]
        public void GetAll_WhenCorrectSelectSpecified_ShouldReturnSuccessfullyMappedObject()
        {
            TestSQLiteDeclarativeAdoObject adoObject = new TestSQLiteDeclarativeAdoObject();

            IEnumerable<TestUserObjectDto> users = adoObject.GetAll();

            Assert.IsNotNull(users);
        }
    }
}