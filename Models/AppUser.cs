using Microsoft.AspNetCore.Identity;

namespace ApotekaBackend.Models
{
    public class AppUser :IdentityUser<int>
    {
     
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; } = [];
    }
}
