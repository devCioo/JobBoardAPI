namespace JobBoardAPI.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<JobAdvertisement> JobAdvertisements { get; set; }
    }
}
