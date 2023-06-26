using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime LastActive { get; set; }
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
