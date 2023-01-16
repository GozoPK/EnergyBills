namespace AppApi.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> list, int pageNumber, int pageSize, int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
            AddRange(list);

        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}