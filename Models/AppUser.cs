using Microsoft.AspNetCore.Identity;

namespace ApotekaBackend.Models
{
    public class AppUser :IdentityUser<int>
    {
     
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; } = [];

        public ICollection<Lek> Lekovi { get; set; } = [];

        public ICollection<Klijent> Klijenti { get; set; } = [];

        public ICollection<Recept>Recepti { get; set; } = [];
    }
}
