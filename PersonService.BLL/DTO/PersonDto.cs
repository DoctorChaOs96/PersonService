using System.ComponentModel.DataAnnotations;

namespace PersonService.BLL.DTO
{
    public class PersonDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        public byte[] Avatar { get; set; }

        public byte[] Resume { get; set; }
    }
}
