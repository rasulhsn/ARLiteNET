
using ARLiteNET.SQLite.Integration.Tests.Data;
using ARLiteNET.SQLite.Integration.Tests.Stub;

namespace ARLiteNET.SQLite.Integration.Tests
{
    [TestClass]
    public class SQLiteRawTests
    {
        [TestInitialize]
        public void Initialize() => SQLiteInMemory.PrepareDBForTest();

        [TestMethod]
        public void GetAll_WhenCalled_ShouldReturnNonNullCollection()
        {
            SQLiteRawStub adoObject = new SQLiteRawStub();

            IEnumerable<UserDtoStub> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }

        [TestMethod]
        public void Add_WhenCorrectInsert_ShouldReturnAddedObjectCount()
        {
            UserDtoStub newUserStub = new()
            {
                Name = "Test",
                IsActive = true,
            };
            bool expectedResult = true;
            SQLiteRawStub adoObject = new SQLiteRawStub();

            bool actualResult = adoObject.Add(newUserStub);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}