namespace AppApi.DTOs
{
    public class BillInfoForAdminToReturnDto
    {
        public string Id { get; set; }
        public string BillNumber { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal Ammount { get; set; }
        public decimal AmmountToReturn { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string State { get; set; }
        public UserToReturnDto User { get; set; }
    }
}