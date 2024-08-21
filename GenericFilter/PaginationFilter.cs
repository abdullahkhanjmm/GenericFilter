namespace GenericFilter
{
    public class PaginationFilter
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; } = 10;
        public string? filterBy { get; set; }
        public string? orderby { get; set; }

        public PaginationFilter()
        {
            pageNumber = 1;
            pageSize = 10;
            filterBy = string.Empty;
        }
    }
}
