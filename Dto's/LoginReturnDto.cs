namespace ApotekaBackend.Dto_s
{
    public class LoginReturnDto
    {


        public string? Name { get; set; }
        public string Surname { get; set; }
        
        public string Email { get; set; }
        public string Phone {  get; set; }  

        public string Role { get; set; }    
        public required string Token { get; set; }


    }
}
