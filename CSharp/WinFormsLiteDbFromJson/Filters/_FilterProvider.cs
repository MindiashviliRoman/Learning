namespace WinFormsLiteDbFromJson.Filters
{
    public class _FilterProvider<TValue> where TValue : IComparable<TValue>, IConvertible
    {
        #region Support type
        public enum TypeFilter
        {
            EQ,
            LE,
            GE,
            CONSISTS,

        }
        #endregion

        Dictionary<TypeFilter, FilterOperation<TValue>> _filters = new Dictionary<TypeFilter, FilterOperation<TValue>>();

        public _FilterProvider()
        {
            _filters.Add(TypeFilter.EQ, new FilterOperationEQ<TValue>());
            _filters.Add(TypeFilter.LE, new FilterOperationLE<TValue>());
            _filters.Add(TypeFilter.GE, new FilterOperationGE<TValue>());
            _filters.Add(TypeFilter.CONSISTS, new FilterOperationConsists<TValue>());
        }

        public FilterOperation<TValue> GetFilter(TypeFilter typeFilter) => _filters[typeFilter];
    }
}
