using ARLiteNET.Core;
using ARLiteNET.Lib.Unit.Tests.Stub;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace ARLiteNET.Unit.Tests
{
    [TestClass]
    public class ExpressionMemberTests
    {
        [TestMethod]
        public void Create_WhenGivenValidPropertyExpression_ShouldReturnPropertyNameByModel()
        {
            const string expected = nameof(UserDtoStub.Id);
            Expression<Func<UserDtoStub, long>> lambda = (x) => x.Id;

            ExpressionMember vMember = ExpressionMember.Create(lambda);

            Assert.AreEqual(expected, vMember.Name);
        }
    }
}
