using Microsoft.AspNetCore.Identity;

namespace ApotekaBackend.Models
{
    public class AppRole:IdentityRole<int>
    {

        public ICollection<AppUserRole> UserRoles { get; set; } = [];
    }
}
