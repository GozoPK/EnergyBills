namespace AppApi.Errors
{
    public class AuthenticationErrorResponse : ErrorResponse
    {
        public AuthenticationErrorResponse(int statusCode, string message = null) : base(statusCode, message)
        {
        }

        public bool FailedToAuthenticate { get; set; } = true;
    }
}