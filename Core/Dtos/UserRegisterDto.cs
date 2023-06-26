using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 4)]
        public string Password { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Unknown
    }
}
