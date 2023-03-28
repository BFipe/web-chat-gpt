using AutoMapper;
using Chat.Business.Interfaces;
using Chat.Business.Models.AccountModels;
using Chat.Entities.DatabaseEntities.GPTUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;

namespace Chat.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<GPTUser> _userManager;
        private readonly ILogger<AccountService> _logger;

        private GPTUser _user;

        private const string _loginProvider = "Chat.API";
        private const string _refreshToken = "RefreshToken";
        public AccountService(IMapper mapper, UserManager<GPTUser> userManager, ILogger<AccountService> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IEnumerable<IdentityError>> Register(RegisterDto registerUserDto)
        {
            List<IdentityError> resultErrors = new List<IdentityError>();
            _user = _mapper.Map<GPTUser>(registerUserDto);
            _user.UserName = registerUserDto.Email;

            var result = await _userManager.CreateAsync(_user, registerUserDto.Password);

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(_user, "User");
                if (addToRoleResult.Succeeded == false)
                {
                    resultErrors.AddRange(addToRoleResult.Errors);
                }
            }
            else
            {
                resultErrors.AddRange(result.Errors);
            }

            return resultErrors;
        }
        public async Task<AuthDto> Login(LoginDto loginUserDto)
        {
            bool isValid = false;

            _user = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (_user == null)
            {
                return null;
            }
            isValid = await _userManager.CheckPasswordAsync(_user, loginUserDto.Password);

            if (isValid)
            {
                var token = await GenerateToken();
                return new AuthDto()
                {
                    UserEmail = _user.Email,
                    Token = token,
                    RefreshToken = await CreateRefreshToken()
                };
            }
            else
            {
                return null;
            }

        }
        public async Task<string> CreateRefreshToken()
        {
            try
            {
                await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
                var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
                var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
                if (result.Succeeded)
                {
                    return newRefreshToken;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (_user == null)
                {
                    _logger.LogError(ex, $"Error in the {nameof(CreateRefreshToken)} while trying to create refresh token for null user");
                }
                else
                {
                    _logger.LogError(ex, $"Something went wrong in the {nameof(CreateRefreshToken)} while trying to create refresh token for user {_user.Email}");
                }
                throw;
            }
        }

        public async Task<AuthDto> VerifyRefreshToken(AuthDto request)
        {
            try
            {
                var jwtSequrityTokenHandler = new JwtSecurityTokenHandler();
                if (jwtSequrityTokenHandler.CanReadToken(request.Token) == false)
                {
                    return null;
                }

                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken();

                try
                {
                    var tokenContent = jwtSequrityTokenHandler.ReadJwtToken(request.Token);
                    jwtSecurityToken = tokenContent;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Invalid token provided while trying to refresh it for user {request.UserEmail}");
                    return null;
                }

                var username = jwtSecurityToken.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Sub)?.Value;
                _user = await _userManager.FindByNameAsync(username);
                if (_user is null || _user.Email != request.UserEmail)
                {
                    return null;
                }

                var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);
                if (isValidRefreshToken)
                {
                    var refreshToken = await CreateRefreshToken();
                    if (refreshToken is not null)
                    {
                        var token = await GenerateToken();
                        return new AuthDto
                        {
                            Token = token,
                            UserEmail = _user.Email,
                            RefreshToken = refreshToken,
                        };
                    }
                }

                await _userManager.UpdateSecurityStampAsync(_user);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateRefreshToken)} while trying to verify refresh token for user {_user.Email}");
                throw;
            }
        }

        private async Task<string> GenerateToken()
        {
            var sequrityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTSecretKey")));

            var credentials = new SigningCredentials(sequrityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, _user.Id),
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWTIssuer"),
                audience: Environment.GetEnvironmentVariable("JWTAudience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(Environment.GetEnvironmentVariable("JWTDuration"))),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
