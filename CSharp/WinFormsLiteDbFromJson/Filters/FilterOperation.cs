namespace WinFormsLiteDbFromJson.Filters
{
    public abstract class FilterOperation<TValue>
    {
        public TValue FilterValue { get; private set; }

        public void SetFilter(TValue value)
        {
            FilterValue = value;
        }

        public abstract bool CheckFilter(TValue value);
    }
}
