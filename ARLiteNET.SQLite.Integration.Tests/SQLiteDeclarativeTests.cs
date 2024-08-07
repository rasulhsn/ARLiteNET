
using ARLiteNET.SQLite.Integration.Tests.Data;
using ARLiteNET.SQLite.Integration.Tests.Stub;

namespace ARLiteNET.SQLite.Integration.Tests
{
    [TestClass]
    public class SQLiteDeclarativeTests
    {
        [TestInitialize]
        public void Initialize() => SQLiteInMemory.PrepareTestDB();

        [TestMethod]
        public void GetAll_WhenCorrectSelectSpecified_ShouldReturnSuccessfullyMappedObject()
        {
            SQLiteDeclarativeStub adoObject = new SQLiteDeclarativeStub();

            IEnumerable<UserDtoStub> users = adoObject.GetAll();

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
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
            SQLiteDeclarativeStub adoObject = new SQLiteDeclarativeStub();

            bool actualResult = adoObject.Add(newUserStub);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetByName_WhenCorrectWhereClauseWithCondition_ShouldReturnSuccessfullyMappedObject()
        {
            const string name = "Test";
            SQLiteDeclarativeStub adoObject = new SQLiteDeclarativeStub();

            IEnumerable<UserDtoStub> users = adoObject.GetByName(name);

            Assert.IsNotNull(users);
        }
    }
}