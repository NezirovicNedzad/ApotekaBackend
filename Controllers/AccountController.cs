using ApotekaBackend.Data;
using ApotekaBackend.Dto_s;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using ApotekaBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ApotekaBackend.Controllers
{

    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IAuthService _authService) : BaseApiController
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
            var (result, error) = await _authService.Register(registerDto);

            if (result == null)
                return BadRequest(error);

            return Ok(result);
        }
    

        [HttpPost("login")]

        public async Task<ActionResult<LoginReturnDto>> Login(UserLoginDto loginDto)
        {


            var (result, error) = await _authService.Login(loginDto);

            if (result == null)
                return Unauthorized(error);

            return Ok(result);
        }

     



    }
}
