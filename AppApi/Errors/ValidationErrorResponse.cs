namespace AppApi.Errors
{
    public class ValidationErrorResponse : ErrorResponse
    {
        public ValidationErrorResponse() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}