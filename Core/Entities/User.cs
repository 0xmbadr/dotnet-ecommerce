using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime LastActive { get; set; }
        public string Name { get; set; }
    }
}
