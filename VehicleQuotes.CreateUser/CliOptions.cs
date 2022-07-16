using CommandLine;
using CommandLine.Text;

namespace VehicleQuotes.CreateUser;

class CliOptions
{
    [Value(0, Required = true, MetaName = "username", HelpText = "The username of the new user account to create.")]
    public string Username { get; set; }

    [Value(1, Required = true, MetaName = "email", HelpText = "The email of the new user account to create.")]
    public string Email { get; set; }

    [Value(2, Required = true, MetaName = "password", HelpText = "The password of the new user account to create.")]
    public string Password { get; set; }

    [Usage(ApplicationAlias = "create_user")]
    public static IEnumerable<Example> Examples
    {
        get
        {
            return new List<Example> {
                new (
                    "Create a new user account",
                    new CliOptions { Username = "name", Email = "email@domain.com", Password = "secret" }
                )
            };
        }
    }
}
