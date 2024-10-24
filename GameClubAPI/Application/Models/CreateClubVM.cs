using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class CreateClubVM
    {
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
