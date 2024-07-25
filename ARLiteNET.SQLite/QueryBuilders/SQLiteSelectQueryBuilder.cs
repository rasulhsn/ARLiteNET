using ARLiteNET.Common;
using System.Collections.Generic;
using System.Linq;

namespace ARLiteNET.SQLite
{
    public class SQLiteSelectQueryBuilder : ChainQueryBuilder,
                                                ISelectQueryBuilder
    {
        const string SELECT = "SELECT";
        const string ALL = "*";

        private IEnumerable<string> _columns;
        private bool HasColumns => _columns != null && _columns.Any();

        public SQLiteSelectQueryBuilder(params string[] columns) : base(null) => _columns = columns;

        public ISelectQueryBuilder Select(params string[] columns)
        {
            _columns = columns;
            return this;
        }

        public ISelectQueryBuilder Select(IEnumerable<string> columns)
        {
            _columns = columns;
            return this;
        }

        public IFromQueryBuilder From(string table) 
            => new SQLiteFromQueryBuilder(table, this);

        public IJoinQueryBuilder Join(string table)
        {
            throw new System.NotImplementedException();
        }

        protected override string Build(QueryBuilderContext? context = null)
        {
            if (HasColumns)
            {
                string aggregatedColumn = string.Empty;

                if (context.HasValue)
                {
                    IEnumerable<string> columns = _columns.Select(x => $"{context.Value.Alias}.{x}");
                    aggregatedColumn = string.Join(",", columns);
                }
                else
                {
                    aggregatedColumn = string.Join(",", _columns);
                }

                return $"{SELECT} {aggregatedColumn} ";
            }

            return $"{SELECT} {ALL} ";
        }

        public string Build() => Build(null);
    }
}
