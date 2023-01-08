namespace AppApi.Errors
{
    public class ErrorException : ErrorResponse
    {
        public ErrorException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}