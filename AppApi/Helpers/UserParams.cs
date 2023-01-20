namespace AppApi.Helpers
{
    public class UserParams : PaginationParams
    {
        public string Username { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
        public int MinMonth { get; set; } = 1;
        public int MinYear { get; set; } = 2022;
        public int MaxMonth { get; set; } = DateTime.Now.Month;
        public int MaxYear { get; set; } = DateTime.Now.Year;
        public string OrderBy { get; set; } = "dateLatest";

    }
}