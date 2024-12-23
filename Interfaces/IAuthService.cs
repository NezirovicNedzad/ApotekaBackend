using ApotekaBackend.Dto_s;

namespace ApotekaBackend.Interfaces
{
    public interface IAuthService
    {

        Task<(LoginReturnDto? Result, string? Error)> Login(UserLoginDto userLoginDto);
        Task<(LoginReturnDto? Result, string? Error)> Register(UserRegisterDto userRegisterDto);
        Task<bool> UserExists(string email);
    }
}
