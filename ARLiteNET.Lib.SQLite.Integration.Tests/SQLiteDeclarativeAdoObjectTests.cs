using ARLiteNET.Lib.SQLite.Tests.Data.InMemory;
using ARLiteNET.Lib.Tests.Data.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteDeclarativeAdoObjectTests
    {
        public SQLiteDeclarativeAdoObjectTests()
        {
            SQLiteInMemory.PrepareDB();
        }

        [TestMethod]
        public void GetAll_WhenCorrectSelectSpecified_ShouldReturnSuccessfullyMappedObject()
        {
            SQLiteDeclarativeAdoObjectStub adoObject = new SQLiteDeclarativeAdoObjectStub();

            IEnumerable<UserObjectDtoStub> users = adoObject.GetAll();

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        public void Add_WhenCorrectInsert_ShouldReturnAddedObjectCount()
        {
            UserObjectDtoStub newUserStub = new()
            {
                Name = "Test",
                IsActive = true,
            };
            bool expectedResult = true;
            SQLiteDeclarativeAdoObjectStub adoObject = new SQLiteDeclarativeAdoObjectStub();

            bool actualResult = adoObject.Add(newUserStub);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}