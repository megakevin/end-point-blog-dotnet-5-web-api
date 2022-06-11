using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VehicleQuotes.Models;
using VehicleQuotes.ResourceModels;
using VehicleQuotes.Services;

namespace VehicleQuotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtService;
        private readonly ApiKeyService _apiKeyService;

        public UsersController(
            UserManager<IdentityUser> userManager,
            JwtService jwtService,
            ApiKeyService apiKeyService 
        ) {
            _userManager = userManager;
            _jwtService = jwtService;
            _apiKeyService = apiKeyService;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(
                new IdentityUser() { UserName = user.UserName, Email = user.Email },
                user.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            user.Password = null;
            return CreatedAtAction("GetUser", new { username = user.UserName }, user);
        }

        // GET: api/Users/username
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            IdentityUser user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return new User
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        // POST: api/Users/BearerToken
        [HttpPost("BearerToken")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(AuthenticationRequest request)
        {
            return await CreateAuthToken(request, user => _jwtService.CreateToken(user));
        }

        // POST: api/Users/ApiKey
        [HttpPost("ApiKey")]
        public async Task<ActionResult<UserApiKey>> CreateApiKey(AuthenticationRequest request)
        {
            return await CreateAuthToken(request, user => _apiKeyService.CreateApiKey(user));
        }

        private async Task<ActionResult<TAuthTokenType>> CreateAuthToken<TAuthTokenType>(
            AuthenticationRequest request,
            Func<IdentityUser, TAuthTokenType> createToken
        ) {
            var user = await Authenticate(request);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var token = createToken(user);

            return Ok(token);
        }

        private async Task<IdentityUser> Authenticate(AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return null;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return null;
            }

            return user;
        }
    }
}
