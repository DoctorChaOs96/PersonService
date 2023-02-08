using System.ComponentModel.DataAnnotations;

namespace PesonService.DAL.Entity
{
    public class PersonEntity : BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        public byte[]? Avatar { get; set; }

        public byte[]? Resume { get; set; }
    }
}
