using ARLiteNET.Lib.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace ARLiteNET.Lib.Core.ExpressionHelpers
{
    /// <summary>
    /// Factory for pooling <see cref="ExpressionMember"/>.
    /// </summary>
    public static class ExpressionMemberFactory
    {
        private static ConcurrentDictionary<int, ExpressionMember> _membersPool { get; }

        /// <summary>
        /// Initializes the <see cref="ExpressionMemberFactory"/> class.
        /// </summary>
        static ExpressionMemberFactory()
        {
            _membersPool = new ConcurrentDictionary<int, ExpressionMember>();
        }

        /// <summary>
        /// Creates for <see cref="{T}"/> instance.
        /// </summary>
        public static ExpressionMember Create<T>()
        {
            Type type = typeof(T);
            Expression<Func<T, T>> expression = x => x;
            ExpressionMember newMember = new ExpressionMember(expression, type, type);
            return newMember;
        }

        /// <summary>
        /// Creates the specified expression.
        /// </summary>
        public static ExpressionMember Create<T, TMember>(Expression<Func<T, TMember>> expression, bool inPool = true)
        {
            if (expression == null)
                throw new ARLiteException(nameof(ExpressionMemberFactory),
                                            new ArgumentNullException(nameof(expression)));

            ExpressionMember createExpMember() => new ExpressionMember(expression, typeof(T), typeof(TMember));

            if (inPool)
            {
                int expHashCode = CreateHashCode(typeof(T), expression);
                if (_membersPool.TryGetValue(expHashCode, out ExpressionMember member))
                {
                    return member;
                }
                else
                {
                    var newMember = createExpMember();
                    _membersPool.TryAdd(expHashCode, newMember);
                    return newMember;
                }
            }
            else
            {
                return createExpMember();
            }
        }

        private static int CreateHashCode(Type type, Expression expression)
        {
            string expStr = expression.ToString();
            string nameStr = type.FullName.ToString();
            return $"{expStr}{nameStr}".GetHashCode();
        }
    }
}
