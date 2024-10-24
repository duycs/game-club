using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class CreateClubEventVM
    {
        [Required(ErrorMessage = "The title is required")]        
        public string Title { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "The scheduled is required")]
        public DateTime Scheduled { get; set; }
    }
}
