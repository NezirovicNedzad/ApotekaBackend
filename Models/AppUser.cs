namespace ApotekaBackend.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string Surname { get; set; } 
        public string Email { get; set; } 
        public string Phone { get; set; }   
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }   
    
    }
}
