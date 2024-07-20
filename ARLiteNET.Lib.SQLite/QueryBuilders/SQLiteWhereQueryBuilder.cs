using ARLiteNET.Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARLiteNET.Lib.SQLite
{
    public class SQLiteWhereQueryBuilder : ChainQueryBuilder,
                                            IWhereQueryBuilder,
                                            IConditionQueryBuilder<IWhereQueryBuilder>
    {
        const string WHERE = "WHERE";

        readonly Dictionary<string, string> Symbols = new Dictionary<string, string>()
        {
            {nameof(IWhereQueryBuilder.EqualTo), "="},
            {nameof(IWhereQueryBuilder.GreaterThan), ">"},
            {nameof(IWhereQueryBuilder.LessThan), "<"},
            {nameof(IWhereQueryBuilder.In), "IN"},
            {nameof(IConditionQueryBuilder<IWhereQueryBuilder>.And), "AND"},
            {nameof(IConditionQueryBuilder<IWhereQueryBuilder>.Or), "OR"},
        };

        private readonly string _firstColumn;
        private readonly string _alias;
        private readonly List<string> _conditions;

        public SQLiteWhereQueryBuilder(string alias, string firstColumn, ChainQueryBuilder innerQueryBuilder) : base(innerQueryBuilder)
        {
            _firstColumn = firstColumn ?? throw new ArgumentNullException(nameof(firstColumn));
            _alias = alias ?? throw new ArgumentNullException(nameof(alias));
            
            _conditions = new List<string>
            {
                $"{WHERE} {GetColumnName(_firstColumn)} "
            };
        }

        public IConditionQueryBuilder<IWhereQueryBuilder> EqualTo(string value)
        {
            _conditions.Add($"{Symbols[nameof(IWhereQueryBuilder.EqualTo)]} '{value}' ");
            return this;
        }

        public IConditionQueryBuilder<IWhereQueryBuilder> GreaterThan(int value)
        {
            _conditions.Add($"{Symbols[nameof(IWhereQueryBuilder.GreaterThan)]} {value} ");
            return this;
        }

        public IConditionQueryBuilder<IWhereQueryBuilder> GreaterThan(decimal value)
        {
            _conditions.Add($"{Symbols[nameof(IWhereQueryBuilder.GreaterThan)]} {value} ");
            return this;
        }

        public IConditionQueryBuilder<IWhereQueryBuilder> In(params string[] values)
        {
            IEnumerable<string> convertedStrings = values.Select(x => $"'{x}'");
            string joinedValues = string.Join(",", convertedStrings);

            _conditions.Add($"{Symbols[nameof(IWhereQueryBuilder.In)]} ({joinedValues}) ");
            return this;
        }

        public IConditionQueryBuilder<IWhereQueryBuilder> LessThan(int value)
        {
            _conditions.Add($"{Symbols[nameof(IWhereQueryBuilder.LessThan)]} {value} ");
            return this;
        }

        public IConditionQueryBuilder<IWhereQueryBuilder> LessThan(decimal value)
        {
            _conditions.Add($"{Symbols[nameof(IWhereQueryBuilder.LessThan)]} {value} ");
            return this;
        }

        public IOrderByQueryBuilder OrderBy()
        {
            throw new NotImplementedException();
        }

        public IWhereQueryBuilder And(string column)
        {
            _conditions.Add($"{Symbols[nameof(IConditionQueryBuilder<IWhereQueryBuilder>.And)]} {GetColumnName(column)} ");
            return this;
        }

        public IWhereQueryBuilder Or(string column)
        {
            _conditions.Add($"{Symbols[nameof(IConditionQueryBuilder<IWhereQueryBuilder>.Or)]} {GetColumnName(column)} ");
            return this;
        }

        protected override string Build(QueryBuilderContext? context = null)
        {
            StringBuilder builder = new StringBuilder();

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
        {
            return Build(null);
        }

        private string GetColumnName(string column)
            => $"{_alias}.{column}";
    }
}
