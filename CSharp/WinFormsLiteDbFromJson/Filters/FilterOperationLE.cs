namespace WinFormsLiteDbFromJson.Filters
{
    public class FilterOperationLE<TValue> : FilterOperation<TValue> where TValue : IComparable<TValue>
    {
        public readonly string Name = "LE";

        public override bool CheckFilter(TValue value)
        {
            return FilterValue.CompareTo(value) <= 0;
        }
    }
}
