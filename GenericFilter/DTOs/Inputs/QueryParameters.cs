namespace GenericFilter.DTOs.Inputs
{
    public class QueryParameters
    {
        /// <summary>
        /// The field by which to sort the data.
        /// </summary>
        public string SortBy { get; set; }
        /// <summary>
        /// Indicates whether the sorting should be ascending or descending.
        /// </summary>
        public bool IsAscending { get; set; }
        /// <summary>
        /// The field(s) by which to filter the data.
        /// </summary>
        public List<string> FilterBy { get; set; }
        /// <summary>
        /// The value to use for filtering the specified fields.
        /// </summary>
        public string FilterValue { get; set; }
        /// <summary>
        /// The operation to use for filtering (e.g., Equals, Contains).
        /// </summary>
        public FilterOperations FilterOperation { get; set; }
        /// <summary>
        /// The page number to retrieve.
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// The number of items to retrieve per page.
        /// </summary>
        public int PageSize { get; set; }
    }

    public enum FilterOperations
    {
        Equals,
        NotEquals,
        Contains,
        StartsWith,
        EndsWith,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual
    }

}
