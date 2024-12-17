using ApotekaBackend.Data;
using ApotekaBackend.Dto_s;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ApotekaBackend.Controllers
{
  
    public class AccountController(UserManager<AppUser> userManager,ITokenService tokenService) : BaseApiController
    {
        [HttpGet]

        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {

            var users = await userManager.Users.ToListAsync();
            return users;

        }

       
        [HttpGet("{id:int}")]

        public async Task<ActionResult<AppUser>> GetUser(int id)
        {

            var user = await userManager.Users.FirstAsync(x => x.Id == id); 


            if (user == null) return NotFound();
            return user;

        }


        [HttpPost("register")]
        public async Task<ActionResult<LoginReturnDto>> Register(UserRegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email)) return BadRequest("Email is taken!Try with another email");

            

            var user = new AppUser
            {
               Name =registerDto.Name, 
               Surname = registerDto.Surname, 
               Phone = registerDto.Phone,
               UserName=registerDto.Username,
               Email = registerDto.Email.ToLower(),
              
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return new LoginReturnDto { 
                Email=user.Email,
                Name=user.Name,
                Surname=user.Surname, 
                Phone=user.Phone,
                Token=await tokenService.CreateToken(user)
                };

        }

        [HttpPost("login")]
        
        public async Task<ActionResult<LoginReturnDto>>Login(UserLoginDto loginDto)
        {


            var user=await userManager.Users.
                FirstOrDefaultAsync(x=>x.Email==loginDto.email.ToUpper());

            if (user == null || user.Email==null) return Unauthorized("Invalid email");



            var result = await userManager.CheckPasswordAsync(user, loginDto.password);

            if (!result) return Unauthorized();
              return new LoginReturnDto
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Phone = user.Phone,
                Token = await tokenService.CreateToken(user)
            };

        }

        private async Task<bool>UserExists(string email)
        {

            return await userManager.Users.AnyAsync(x => x.NormalizedEmail == email.ToUpper()); 
        }
        
      
       
        
    }
}
