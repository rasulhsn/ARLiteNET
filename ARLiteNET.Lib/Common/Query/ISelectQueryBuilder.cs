using ARLiteNET.Lib.Common.Query;
using System.Collections.Generic;

namespace ARLiteNET.Lib.Common
{
    public interface ISelectQueryBuilder : IQueryBuilder
    {
        ISelectQueryBuilder Select(params string[] columns);
        ISelectQueryBuilder Select(IEnumerable<string> columns);
        IFromQueryBuilder From(string table);
        IJoinQueryBuilder Join(string table);
        IOrderByQueryBuilder OrderBy(string columnName);
    }
}
