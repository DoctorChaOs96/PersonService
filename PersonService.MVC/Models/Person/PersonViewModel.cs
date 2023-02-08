using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonService.MVC.Models.Person
{
    public class PersonViewModel
    {
        [MaxLength(50)]
        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DisplayName("Date of birth")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Avatar")]
        public IFormFile Avatar { get; set; }

        [DisplayName("CV")]
        public IFormFile Resume { get; set; }
    }
}
