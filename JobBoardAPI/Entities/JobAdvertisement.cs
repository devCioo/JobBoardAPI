namespace JobBoardAPI.Entities
{
    public class JobAdvertisement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public string? Description { get; set; }
        public string Responsibilities { get; set; }
        public string? Requirements { get; set; }
        public DateTime PostedOn { get; set; }
        public virtual List<JobApplication> JobApplications { get; set; }
    }
}
