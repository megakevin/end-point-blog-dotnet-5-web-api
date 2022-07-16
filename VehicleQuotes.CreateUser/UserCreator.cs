using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace VehicleQuotes.CreateUser;

class UserCreator
{
    private readonly ILogger<UserCreator> _logger;
    private readonly VehicleQuotesContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public UserCreator(
        ILogger<UserCreator> logger,
        VehicleQuotesContext context,
        UserManager<IdentityUser> userManager
    ) {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public IdentityResult Run(string username, string email, string password)
    {
        var userCreateTask = _userManager.CreateAsync(
            new IdentityUser() { UserName = username, Email = email },
            password
        );

        var result = userCreateTask.Result;

        return result;
    }
}
