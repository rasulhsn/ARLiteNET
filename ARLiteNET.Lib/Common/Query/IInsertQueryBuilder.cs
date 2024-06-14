namespace ARLiteNET.Lib.Common
{
    public enum InsertDataType: byte
    {
        NULL, INTEGER, REAL, TEXT, BLOB
    }

    public struct InsertValueObject
    {
        public string Column { get; }
        public object Value { get; }
        public InsertDataType DataType { get; }

        public InsertValueObject(string column, object value, InsertDataType dataType)
        {
            Column = column;
            Value = value;
            DataType = dataType;
        }
    }

    public interface IInsertQueryBuilder : IQueryBuilder
    {
        IInsertQueryBuilder Value(params InsertValueObject[] valuesInfo);
    }
}
