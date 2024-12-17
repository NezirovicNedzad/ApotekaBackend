using System.ComponentModel.DataAnnotations;

namespace ApotekaBackend.Dto_s
{
    public class UserRegisterDto
    {

        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; }=string.Empty;
        [Required]
        public string Email { get; set; }= string.Empty;
        public string Phone { get; set; }= string.Empty;
        [Required]
        [StringLength(8,MinimumLength =4)]       
        public string Password { get; set; }= string.Empty;
        [Required]
        public string Username { get; set; }= string.Empty;
    }
}
