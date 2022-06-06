using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VehicleQuotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(
            UserManager<IdentityUser> userManager
        ) {
            _userManager = userManager;
        }
    }
}
