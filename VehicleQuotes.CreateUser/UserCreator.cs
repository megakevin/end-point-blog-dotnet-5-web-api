using Microsoft.AspNetCore.Identity;

namespace VehicleQuotes.CreateUser;

class UserCreator
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserCreator(UserManager<IdentityUser> userManager) {
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
