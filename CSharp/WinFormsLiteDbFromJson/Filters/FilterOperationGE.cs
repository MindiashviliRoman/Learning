namespace WinFormsLiteDbFromJson.Filters
{
    public class FilterOperationGE<TValue> : FilterOperation<TValue> where TValue : IComparable<TValue>
    {
        public readonly string Name = "GE";

        public override bool CheckFilter(TValue value)
        {
            return FilterValue.CompareTo(value) >= 0;
        }
    }
}
