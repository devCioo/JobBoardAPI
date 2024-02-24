namespace JobBoardAPI.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string CoverLetter { get; set; }
        public DateTime AppliedOn { get; set; }
        public int JobAdvertisementId { get; set; }
        public virtual JobAdvertisement JobAdvertisement { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
