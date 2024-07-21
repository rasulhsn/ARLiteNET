
using ARLiteNET.Common;
using ARLiteNET.SQLite.Integration.Tests.Stub;

namespace ARLiteNET.SQLite.Integration.Tests
{
    [TestClass]
    public class SQLiteQueryBuilderTests
    {
        [TestMethod]
        public void Build_WhenCalledSelectWithAllColumns_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"SELECT * FROM {nameof(UserDtoStub)} ";

            string generatedQuery = SQLiteQueryFactory.Select()
                                .From(nameof(UserDtoStub))
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledSelectAndWhereCondition_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"SELECT * FROM {nameof(UserDtoStub)} AS T WHERE T.Name = 'Test' AND T.Id > 0 ";

            string generatedQuery = SQLiteQueryFactory.Select()
                                .From(nameof(UserDtoStub))
                                .Alias("T")
                                .Where(nameof(UserDtoStub.Name))
                                .EqualTo("Test")
                                .And(nameof(UserDtoStub.Id))
                                .GreaterThan(0)
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledSelectAndConditionWithSpecifiedColumns_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"SELECT T.Id,T.Name FROM {nameof(UserDtoStub)} AS T WHERE T.Name = 'Test' AND T.Id > 0 ";

            string generatedQuery = SQLiteQueryFactory.Select("Id","Name")
                                .From(nameof(UserDtoStub))
                                .Alias("T")
                                .Where(nameof(UserDtoStub.Name))
                                .EqualTo("Test")
                                .And(nameof(UserDtoStub.Id))
                                .GreaterThan(0)
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledInsertWithValues_ShouldReturnCorrectQuery()
        {
            const string nameRaw = "Rasul";
            const string isActiveRaw = "1";
            const string expectedQuery = $"INSERT INTO {nameof(UserDtoStub)} ({nameof(UserDtoStub.Name)},{nameof(UserDtoStub.IsActive)}) VALUES ('{nameRaw}',{isActiveRaw}), ('{nameRaw}',{isActiveRaw})";

            string generatedQuery = SQLiteQueryFactory.Insert(nameof(UserDtoStub))
                                                .Value(new InsertValueObject(nameof(UserDtoStub.Name), nameRaw, InsertDataType.TEXT)
                                                       ,new InsertValueObject(nameof(UserDtoStub.IsActive), isActiveRaw, InsertDataType.INTEGER))
                                                .Value(new InsertValueObject(nameof(UserDtoStub.Name), nameRaw, InsertDataType.TEXT)
                                                       ,new InsertValueObject(nameof(UserDtoStub.IsActive), isActiveRaw, InsertDataType.INTEGER))
                                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledDeleteWithoutConditions_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"DELETE FROM {nameof(UserDtoStub)} ";

            string generatedQuery = SQLiteQueryFactory.Delete(nameof(UserDtoStub))
                                                      .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledDeleteWithConditions_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"DELETE FROM {nameof(UserDtoStub)} WHERE Id > 5 ";

            string generatedQuery = SQLiteQueryFactory.Delete(nameof(UserDtoStub))
                                                      .Where(nameof(UserDtoStub.Id))
                                                      .GreaterThan(5)
                                                      .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }
    }
}
