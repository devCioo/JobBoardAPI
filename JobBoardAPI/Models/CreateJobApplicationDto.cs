using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public class CreateJobApplicationDto
    {
        [Required]
        public string CoverLetter { get; set; }
    }
}
