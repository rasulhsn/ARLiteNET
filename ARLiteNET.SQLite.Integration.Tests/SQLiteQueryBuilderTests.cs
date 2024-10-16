﻿
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
                                                .Value(new ColumnValueObject(nameof(UserDtoStub.Name), nameRaw, DataType.TEXT)
                                                       ,new ColumnValueObject(nameof(UserDtoStub.IsActive), isActiveRaw, DataType.INTEGER))
                                                .Value(new ColumnValueObject(nameof(UserDtoStub.Name), nameRaw, DataType.TEXT)
                                                       ,new ColumnValueObject(nameof(UserDtoStub.IsActive), isActiveRaw, DataType.INTEGER))
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
        public void Build_WhenCalledDeleteWithGreaterThanCondition_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"DELETE FROM {nameof(UserDtoStub)} WHERE Id > 5 ";

            string generatedQuery = SQLiteQueryFactory.Delete(nameof(UserDtoStub))
                                                      .Where(nameof(UserDtoStub.Id))
                                                      .GreaterThan(5)
                                                      .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledDeleteWithEqualToCondition_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"DELETE FROM {nameof(UserDtoStub)} WHERE Id = 5 ";

            string generatedQuery = SQLiteQueryFactory.Delete(nameof(UserDtoStub))
                                                      .Where(nameof(UserDtoStub.Id))
                                                      .EqualTo(5)
                                                      .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledOrderByWithAscendingOrder_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"SELECT * FROM {nameof(UserDtoStub)} ORDER BY {nameof(UserDtoStub.Id)} ASC ";

            string generatedQuery = SQLiteQueryFactory.Select()
                                                       .From(nameof(UserDtoStub))
                                                       .OrderBy()
                                                       .Asc(nameof(UserDtoStub.Id))
                                                       .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledOrderByWithDescendingOrder_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"SELECT * FROM {nameof(UserDtoStub)} ORDER BY {nameof(UserDtoStub.Id)} DESC ";

            string generatedQuery = SQLiteQueryFactory.Select()
                                                       .From(nameof(UserDtoStub))
                                                       .OrderBy()
                                                       .Desc(nameof(UserDtoStub.Id))
                                                       .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledUpdateWithoutConditions_ShouldReturnCorrectQuery()
        {
            const string isActiveRaw = "0";
            const string nameRaw = "rasul";
            const string expectedQuery = $"UPDATE {nameof(UserDtoStub)} SET {nameof(UserDtoStub.IsActive)} = {isActiveRaw},{nameof(UserDtoStub.Name)} = '{nameRaw}' ";

            string generatedQuery = SQLiteQueryFactory.Update(nameof(UserDtoStub))
                                                      .Set(new ColumnValueObject(nameof(UserDtoStub.IsActive), isActiveRaw, DataType.INTEGER))
                                                      .Set(new ColumnValueObject(nameof(UserDtoStub.Name), nameRaw, DataType.TEXT))
                                                      .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledUpdateWithEqualToCondition_ShouldReturnCorrectQuery()
        {
            const string isActiveRaw = "0";
            const string nameRaw = "rasul";
            const string expectedQuery = $"UPDATE {nameof(UserDtoStub)} SET {nameof(UserDtoStub.IsActive)} = {isActiveRaw},{nameof(UserDtoStub.Name)} = '{nameRaw}' WHERE Id = 5 ";

            string generatedQuery = SQLiteQueryFactory.Update(nameof(UserDtoStub))
                                                      .Set(new ColumnValueObject(nameof(UserDtoStub.IsActive), isActiveRaw, DataType.INTEGER))
                                                      .Set(new ColumnValueObject(nameof(UserDtoStub.Name), nameRaw, DataType.TEXT))
                                                      .Where(nameof(UserDtoStub.Id))
                                                      .EqualTo(5)
                                                      .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }
    }
}
