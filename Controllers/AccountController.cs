using ApotekaBackend.Data;
using ApotekaBackend.Dto_s;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ApotekaBackend.Controllers
{
  
    public class AccountController(DataContext context,ITokenService tokenService) : BaseApiController
    {
        [HttpGet]

        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {

            var users = await context.Users.ToListAsync();
            return users;

        }

       
        [HttpGet("{id:int}")]

        public async Task<ActionResult<AppUser>> GetUser(int id)
        {

            var user = await context.Users.FindAsync(id);


            if (user == null) return NotFound();
            return user;

        }


        [HttpPost("register")]
        public async Task<ActionResult<LoginReturnDto>> Register(UserRegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email)) return BadRequest("Email is taken!Try with another email");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
               Name =registerDto.Name, 
               Surname = registerDto.Surname, 
               Phone = registerDto.Phone,
               Email = registerDto.Email.ToLower(),
               PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
               PasswordSalt=hmac.Key
            };

            context.Users.Add(user);    
          await  context.SaveChangesAsync();
            return new LoginReturnDto { 
                Email=user.Email,
                Name=user.Name,
                Surname=user.Surname, 
                Phone=user.Phone,
                Token=tokenService.CreateToken(user)
                };

        }

        [HttpPost("login")]
        
        public async Task<ActionResult<LoginReturnDto>>Login(UserLoginDto loginDto)
        {


            var user=await context.Users.FirstOrDefaultAsync(x=>x.Email==loginDto.email.ToLower());

            if (user == null) return Unauthorized("Invalid email"); 
            
            using var hmac=new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for(int i=0; i<computedHash.Length; i++)
            {

                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password!");   
            }


              return new LoginReturnDto
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Phone = user.Phone,
                Token = tokenService.CreateToken(user)
            };

        }

        private async Task<bool>UserExists(string email)
        {

            return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower()); 
        }
        /*
      
       
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AppUser>> UpdateUser(UserRegisterDto registerDto,int id)
        {

            var user= await context.Users.FindAsync(id);  
            
            if (user == null) return NotFound();


            user.Name = registerDto.Name;
            user.Surname = registerDto.Surname;
            user.Email = registerDto.Email;
            user.Password = registerDto.Password;
            user.Phone = registerDto.Phone;

            

         
            await context.SaveChangesAsync();

         
            return user;

        }
        */
    }
}
