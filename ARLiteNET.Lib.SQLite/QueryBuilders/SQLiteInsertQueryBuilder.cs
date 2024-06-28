using ARLiteNET.Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARLiteNET.Lib.SQLite
{
    public class SQLiteInsertQueryBuilder : IInsertQueryBuilder
    {
        const string INSERT = "INSERT INTO";
        const string VALUES = "VALUES";

        private readonly string _tableName;
        private readonly List<InsertValueObject[]> _values;

        public SQLiteInsertQueryBuilder(string table)
        {
            _tableName = table ?? throw new ArgumentNullException(nameof(table));
            _values = new List<InsertValueObject[]>();
        }

        public IInsertQueryBuilder Value(params InsertValueObject[] valuesInfo)
        {
            if (valuesInfo == null)
                throw new ArgumentNullException("Insert value can't be null or empty!");

            _values.Add(valuesInfo);

            return this;
        }

        public string Build()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"{INSERT} {_tableName} (");

            var columns = _values.SelectMany(x => x)
                                    .Select(x => x.Column)
                                    .Distinct();

            builder.AppendJoin(',', columns);

            builder.Append(")");

            builder.Append($" {VALUES}");

            for (int i = 0; i < _values.Count; i++)
            {
                builder.Append(" (");

                var valueArray = _values[i];

                for (int j = 0; j < valueArray.Length; j++)
                {
                    if (valueArray[j].DataType == InsertDataType.TEXT)
                    {
                        if (j == valueArray.Length - 1)
                            builder.Append($"'{valueArray[j].Value}'");
                        else
                            builder.Append($"'{valueArray[j].Value}',");
                    }
                    else if (valueArray[j].DataType == InsertDataType.BOOLEAN)
                    {
                        string literallStr = "NULL";

                        if (bool.TryParse(valueArray[j].Value?.ToString(), out bool convertedBool))
                            literallStr = convertedBool.ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? "1" : "0";

                        if (j == valueArray.Length - 1)
                            builder.Append($"{literallStr}");
                        else
                            builder.Append($"{literallStr},");
                    }
                    else if (valueArray[j].DataType == InsertDataType.NULL)
                    {
                        if (j == valueArray.Length - 1)
                            builder.Append($"NULL");
                        else
                            builder.Append($"NULL,");
                    }
                    else
                    {
                        if (j == valueArray.Length - 1)
                            builder.Append($"{valueArray[j].Value}");
                        else
                            builder.Append($"{valueArray[j].Value},");
                    }
                }

                if (i == _values.Count - 1)
                    builder.Append(")");
                else
                    builder.Append("),");
            }

            return builder.ToString();
        }
    }
}
