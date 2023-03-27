using Chat.Business.Models.AccountModels;
using Microsoft.AspNetCore.Identity;

namespace Chat.Business.Interfaces
{
    public interface IAccountService
    {
        Task<string> CreateRefreshToken();
        Task<AuthDto> Login(LoginDto loginUserDto);
        Task<IEnumerable<IdentityError>> Register(RegisterDto registerUserDto);
        Task<AuthDto> VerifyRefreshToken(AuthDto request);
    }
}