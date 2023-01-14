namespace TaxisNetSimApi.DTOs
{
    public class UserToReturnFromLoginDto : UserToReturnDto
    {
        public string TaxisnetToken { get; set; }
    }
}