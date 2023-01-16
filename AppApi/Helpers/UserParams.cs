namespace AppApi.Helpers
{
    public class UserParams : PaginationParams
    {
        public string Username { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string OrderBy { get; set; } = "dateLatest";
    }
}