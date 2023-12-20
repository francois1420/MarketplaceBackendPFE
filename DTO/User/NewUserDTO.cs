using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.User
{
    public class NewUserDTO
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; } = null!;
    }
}
