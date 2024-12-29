using ApotekaBackend.Dto_s;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApotekaBackend.Services
{
    public class AuthService(UserManager<AppUser> userManager, ITokenService tokenService) : IAuthService
    {

        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ITokenService _tokenService= tokenService;

        public async Task<(LoginReturnDto? Result, string? Error)> Login(UserLoginDto login)
        {
            var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Email.ToUpper() == login.email.ToUpper());
            if (user == null || string.IsNullOrEmpty(user.Email))
                return (null, "Invalid email");

            var passwordValid = await _userManager.CheckPasswordAsync(user, login.password);
            if (!passwordValid)
                return (null, "Invalid password");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            return (new LoginReturnDto
            {
                Id=user.Id,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Role = role,
                Phone = user.Phone,
                Token = await _tokenService.CreateToken(user)
            }, null);
        }

        public async Task<(LoginReturnDto? Result, string? Error)> Register(UserRegisterDto registerDto)
        {

            if (await UserExists(registerDto.Email)) return (null, "Email is taken!Try with another email");



            var user = new AppUser
            {
              
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Phone = registerDto.Phone,
                UserName = registerDto.Username,
                Email = registerDto.Email.ToLower(),

            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return (null, errors);
            }

            return (new LoginReturnDto
            {
                Id=user.Id,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Phone = user.Phone,
                Token = await tokenService.CreateToken(user)
            }, null);

        }

        public async Task<bool> UserExists(string email)
        {
            return await userManager.Users.AnyAsync(x => x.NormalizedEmail == email.ToUpper());
        }
    }
}
