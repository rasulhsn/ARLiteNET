
namespace ARLiteNET.Common
{
    public struct ColumnValueObject
    {
        public string Column { get; }
        public object Value { get; }
        public DataType DataType { get; }

        public ColumnValueObject(string column,
            object value,
            DataType dataType)
        {
            Column = column;
            Value = value;
            DataType = dataType;
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Column.GetHashCode();
        }
    }
}
