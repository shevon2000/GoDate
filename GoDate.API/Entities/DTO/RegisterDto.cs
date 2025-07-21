using System.ComponentModel.DataAnnotations;

namespace GoDate.API.Entities.DTO
{
    public class RegisterDto
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
