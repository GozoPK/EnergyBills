namespace AppApi.Errors
{
    public class ErrorResponse
    {
        public ErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Σφάλμα 400: Κακή Αίτηση",
                401 => "Σφάλμα 401: Δεν έχετε δικαίωμα πρόσβασης",
                404 => "Σφάλμα 404: Δεν Βρέθηκε",
                500 => "Σφάλμα 500: Σφάλμα στον διακομιστή",
                _ => null
            };
        }
    }
}