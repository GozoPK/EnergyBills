namespace AppApi.Helpers
{
    public class PaginationParams
    {
        private const int MAXSIZE = 24;
        private int _pageSize = 6;
        public int PageSize 
        { 
            get => _pageSize;
            set => _pageSize = value > MAXSIZE ? MAXSIZE : value; 
        }
        public int PageNumber { get; set; } = 1;
    }
}