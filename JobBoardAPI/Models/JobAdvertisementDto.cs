namespace JobBoardAPI.Models
{
    public class JobAdvertisementDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? PostalCode { get; set; }
        public string Responsibilities { get; set; }
        public string? Requirements { get; set; }
        public DateTime PostedOn { get; set; }
    }
}
