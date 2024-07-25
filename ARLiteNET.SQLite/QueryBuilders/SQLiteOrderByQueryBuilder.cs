using ARLiteNET.Common;
using System.Collections.Generic;
using System.Text;

namespace ARLiteNET.SQLite.QueryBuilders
{
    public class SQLiteOrderByQueryBuilder : ChainQueryBuilder,
                                                IOrderByQueryBuilder
    {
        const string ORDER_BY = "ORDER BY";
        const string ASC = "ASC";
        const string DESC = "DESC";

        private readonly List<OrderByValue> _orderByValues;

        public SQLiteOrderByQueryBuilder(ChainQueryBuilder innerQueryBuilder) : base(innerQueryBuilder)
        {
            this._orderByValues = new List<OrderByValue>();
        }

        public IOrderByQueryBuilder Asc(string column)
        {
            _orderByValues.Add(new OrderByValue(ASC, column));
            return this;
        }

        public IOrderByQueryBuilder Desc(string column)
        {
            _orderByValues.Add(new OrderByValue(DESC, column));
            return this;
        }

        protected override string Build(QueryBuilderContext? context = null)
        {
            StringBuilder builder = new StringBuilder();

            BuildChain(builder, context);

            builder.Append($"{ORDER_BY} ");

            if (context.HasValue && context.Value.HasAlias)
            {
                for (int i = 0; i < _orderByValues.Count; i++)
                {
                    if (i == (_orderByValues.Count - 1))
                    {
                        builder.Append($"{context.Value.HasAlias}.{_orderByValues[i].ColumnName} {_orderByValues[i].Order} ");
                    }
                    else
                    {
                        builder.Append($"{context.Value.HasAlias}.{_orderByValues[i].ColumnName} {_orderByValues[i].Order}, ");
                    }
                }
            }
            else
            {
                for (int i = 0; i < _orderByValues.Count; i++)
                {
                    if (i == (_orderByValues.Count - 1))
                    {
                        builder.Append($"{_orderByValues[i].ColumnName} {_orderByValues[i].Order} ");
                    }
                    else
                    {
                        builder.Append($"{_orderByValues[i].ColumnName} {_orderByValues[i].Order}, ");
                    }
                }
            }

            return builder.ToString();
        }

        public string Build() => Build(null);

        struct OrderByValue
        {
            public string Order { get; }
            public string ColumnName { get; }

            public OrderByValue(string order, string columnName)
            {
                this.Order = order;
                this.ColumnName = columnName;
            }
        }
    }
}
