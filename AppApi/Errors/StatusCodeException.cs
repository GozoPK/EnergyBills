namespace AppApi.Errors
{
    public class StatusCodeException : Exception
    {
        public StatusCodeException(ErrorResponse error)
        {
            Error = error;
        }

        public ErrorResponse Error { get; set; }
    }
}