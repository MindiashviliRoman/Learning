namespace WinFormsLiteDbFromJson.Filters
{
    public class FilterOperationConsists<TValue> : FilterOperation<TValue> where TValue : IConvertible
    {
        public readonly string Name = "Contains";

        public override bool CheckFilter(TValue value)
        {
            var strVal = value.ToString();
            if (strVal == null && FilterValue == null)
                return true;

            if (strVal != null && FilterValue != null)
            {
                return strVal.Contains(FilterValue.ToString());
            }
            return false;
        }
    }
}
