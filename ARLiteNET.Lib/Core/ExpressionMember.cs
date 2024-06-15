using ARLiteNET.Lib.Exceptions;
using System;
using System.Linq.Expressions;

namespace ARLiteNET.Lib.Core
{
    internal class ExpressionMember
    {
        private readonly Type _parentType;
        private readonly LambdaExpression _memberExpression;

        public static ExpressionMember Create<T, TMember>(Expression<Func<T, TMember>> expression)
        {
            if (expression == null)
                throw new ARLiteException(nameof(ExpressionMember),
                                            new ArgumentNullException(nameof(expression)));

            ExpressionMember createExpMember() => new ExpressionMember(expression, typeof(T), typeof(TMember));

            return createExpMember();
        }

        private ExpressionMember(LambdaExpression expression,
                                        Type parent, Type member)
        {
            _parentType = parent;
            Type = member ?? throw new ARLiteException(nameof(ExpressionMember),
                                                                    new ArgumentNullException(nameof(member)));
            _memberExpression = expression ?? throw new ARLiteException(nameof(ExpressionMember),
                                                                    new ArgumentNullException(nameof(expression)));

            Initialize();
        }

        public bool IsNullable
        {
            get
            {
                if (Type == typeof(string))
                    return true;
                else if (Type.IsClass)
                    return true;
                else
                    return Nullable.GetUnderlyingType(Type) != null;
            }
        }

        public string EndPointName { get; private set; }
        public Type Type { get; }
        public bool IsFieldOrProperty { get; private set; }

        public object ResolveValue(object instance)
        {
            try
            {
                return _memberExpression
                    .Compile()
                    .DynamicInvoke(instance);
            }
            catch
            {
                throw;
            }
        }
        private void Initialize()
        {
            if (_memberExpression.Body.NodeType != ExpressionType.Parameter)
            {
                if (IsDefinedAs(Defination.Class) || IsDefinedAs(Defination.Struct))
                {
                    TrySetDesriptorInfo(_memberExpression);
                }
            }

            EndPointName = string.IsNullOrEmpty(EndPointName) ? "" : EndPointName;
        }
        private bool TrySetDesriptorInfo(LambdaExpression memberExpression)
        {
            if (IsMember(memberExpression.Body, out MemberExpression operand))
            {
                if (operand.NodeType != ExpressionType.Parameter)
                {
                    EndPointName = operand.Member.Name;
                    IsFieldOrProperty = true;
                    return true;
                }
            }
            else if (IsMethod(memberExpression.Body, out MethodCallExpression callOperand))
            {
                if (callOperand.NodeType != ExpressionType.Parameter)
                {
                    EndPointName = callOperand.Method.Name;
                    IsFieldOrProperty = false;
                    return true;
                }
            }

            return false;
        }
        private bool IsMethod(Expression toUp, out MethodCallExpression methodCallExpression)
        {
            if (toUp is MethodCallExpression)
            {
                methodCallExpression = toUp as MethodCallExpression;
                return true;
            }

            methodCallExpression = default;
            return false;
        }
        private bool IsMember(Expression toUp, out MemberExpression memberExpression)
        {
            if (toUp is UnaryExpression upOperand)
            {
                if ((memberExpression = upOperand.Operand as MemberExpression) != null)
                {
                    return true;
                }
            }
            else if ((memberExpression = toUp as MemberExpression) != null)
            {
                return true;
            }

            memberExpression = default;
            return false;
        }
        private bool IsDefinedAs(Defination defination)
        {
            if (_parentType == typeof(string) && defination == Defination.String)
                return true;
            else if (_parentType.IsPrimitive && defination == Defination.Primitive)
                return true;
            else if (_parentType.IsValueType && defination == Defination.Struct)
                return true;
            else if (_parentType.IsClass && defination == Defination.Class)
                return true;

            return false;
        }
        enum Defination : byte
        {
            Class = 1, Struct = 2, String = 3, Primitive = 4, Other = 5
        }
    }
}
