using System.ComponentModel.DataAnnotations;

namespace GoDate.API.Entities.DTO.Auth
{
    public class LoginDto
    {
        public required string UserName { get; set; }

        public required string Password { get; set; }
    }
}
