using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public class CreateJobAdvertisementDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(70)]
        public string CompanyName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(40)]
        public string City { get; set; }
        [Required]
        [MaxLength(60)]
        public string Street { get; set; }
        public string? PostalCode { get; set; }
        [Required]
        public string Responsibilities { get; set; }
        public string? Requirements { get; set; }
    }
}
