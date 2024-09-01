namespace WinFormsLiteDbFromJson.Filters
{
    public class FilterOperationEQ<TValue> : FilterOperation<TValue> where TValue : IComparable<TValue>
    {
        public readonly string Name = "EQ";

        public override bool CheckFilter(TValue value)
        {
            return FilterValue.Equals(value);
        }
    }
}
