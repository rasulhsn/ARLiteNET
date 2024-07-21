using ARLiteNET.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARLiteNET.SQLite.QueryBuilders
{
    public class SQLiteDeleteQueryBuilder : ChainQueryBuilder,
                                            IDeleteQueryBuilder,
                                            IConditionalFunctionQueryBuilder,
                                            IConditionQueryBuilder<IConditionalFunctionQueryBuilder>
    {
        const string DELETE = "DELETE";
        const string WHERE = "WHERE";

        readonly Dictionary<string, string> Symbols = new Dictionary<string, string>()
        {
            {nameof(IConditionalFunctionQueryBuilder.EqualTo), "="},
            {nameof(IConditionalFunctionQueryBuilder.GreaterThan), ">"},
            {nameof(IConditionalFunctionQueryBuilder.LessThan), "<"},
            {nameof(IConditionalFunctionQueryBuilder.In), "IN"},
            {nameof(IConditionQueryBuilder<IConditionalFunctionQueryBuilder>.And), "AND"},
            {nameof(IConditionQueryBuilder<IConditionalFunctionQueryBuilder>.Or), "OR"},
        };

        private readonly string _table;
        private readonly List<string> _conditions;

        public SQLiteDeleteQueryBuilder(string table) : base(null)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _conditions = new List<string>();
        }

        public IConditionQueryBuilder<IConditionalFunctionQueryBuilder> EqualTo(string value)
        {
            _conditions.Add($"{Symbols[nameof(IConditionalFunctionQueryBuilder.EqualTo)]} '{value}' ");
            return this;
        }

        public IConditionQueryBuilder<IConditionalFunctionQueryBuilder> GreaterThan(int value)
        {
            _conditions.Add($"{Symbols[nameof(IConditionalFunctionQueryBuilder.GreaterThan)]} {value} ");
            return this;
        }

        public IConditionQueryBuilder<IConditionalFunctionQueryBuilder> GreaterThan(decimal value)
        {
            _conditions.Add($"{Symbols[nameof(IConditionalFunctionQueryBuilder.GreaterThan)]} {value} ");
            return this;
        }

        public IConditionQueryBuilder<IConditionalFunctionQueryBuilder> In(params string[] values)
        {
            IEnumerable<string> convertedStrings = values.Select(x => $"'{x}'");
            string joinedValues = string.Join(",", convertedStrings);

            _conditions.Add($"{Symbols[nameof(IConditionalFunctionQueryBuilder.In)]} ({joinedValues}) ");
            return this;
        }

        public IConditionQueryBuilder<IConditionalFunctionQueryBuilder> LessThan(int value)
        {
            _conditions.Add($"{Symbols[nameof(IConditionalFunctionQueryBuilder.LessThan)]} {value} ");
            return this;
        }

        public IConditionQueryBuilder<IConditionalFunctionQueryBuilder> LessThan(decimal value)
        {
            _conditions.Add($"{Symbols[nameof(IConditionalFunctionQueryBuilder.LessThan)]} {value} ");
            return this;
        }

        public IConditionalFunctionQueryBuilder And(string column)
        {
            if (string.IsNullOrEmpty(column))
                throw new ArgumentNullException($"Column name is null or empty!");

            _conditions.Add($"{Symbols[nameof(IConditionQueryBuilder<IConditionalFunctionQueryBuilder>.And)]} {GetColumnName(column)} ");
            return this;
        }

        public IConditionalFunctionQueryBuilder Or(string column)
        {
            if (string.IsNullOrEmpty(column))
                throw new ArgumentNullException($"Column name is null or empty!");

            _conditions.Add($"{Symbols[nameof(IConditionQueryBuilder<IConditionalFunctionQueryBuilder>.Or)]} {GetColumnName(column)} ");
            return this;
        }

        public IConditionalFunctionQueryBuilder Where(string column)
        {
            if (string.IsNullOrEmpty(column))
                throw new ArgumentNullException($"Column name is null or empty!");

            _conditions.Add($"{WHERE} {GetColumnName(column)} ");     
            return this;
        }

        public IOrderByQueryBuilder OrderBy(string column)
        {
            throw new NotImplementedException();
        }

        protected override string Build(QueryBuilderContext? context = null)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"{DELETE} FROM {_table} ");

            BuildChain(builder);

            if (_conditions.Any())
            {
                foreach (string condition in _conditions)
                {
                    builder.Append(condition);
                }
            }

            return builder.ToString();
        }

        string IQueryBuilder.Build()
            => Build(null);

        private string GetColumnName(string column)
            => $"{column}";
    }
}
