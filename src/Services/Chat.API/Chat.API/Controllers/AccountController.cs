using Chat.Business.Interfaces;
using Chat.Business.Models.AccountModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterDto userRegistration)
        {
            _logger.LogInformation($"Registration attempt for {userRegistration.Email}");

            var errors = await _accountService.Register(userRegistration);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            else
            {
                _logger.LogInformation($"Sucessfully registered new user {userRegistration.Email}");
                return Ok();
            }
        }

        // POST: api/Account/Login
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginDto userLogin)
        {
            _logger.LogInformation($"Login attempt for {userLogin.Email}");

            var result = await _accountService.Login(userLogin);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }


        // POST: api/Account/RefreshToken
        [HttpPost]
        [Route("RefreshToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken([FromBody] AuthDto request)
        {
            _logger.LogInformation($"Refreshing token attempt for {request.UserEmail}");

            var result = await _accountService.VerifyRefreshToken(request);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: api/Account/Logout
        [HttpPost]
        [Authorize]
        [Route("Logout")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            //Logout
            return Ok();
        }
    }
}
