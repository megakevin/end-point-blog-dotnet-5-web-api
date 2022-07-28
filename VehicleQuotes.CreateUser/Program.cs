using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CommandLine;
using VehicleQuotes.CreateUser;

void Run(CliOptions options)
{
    IHost host = Host.CreateDefaultBuilder(args)
        .UseContentRoot(System.AppContext.BaseDirectory)
        .ConfigureServices((context, services) =>
        {
            var startup = new VehicleQuotes.Startup(context.Configuration);
            startup.ConfigureServices(services);

            services.AddTransient<UserCreator>();
        })
        .Build();

    var userCreator = host.Services.GetRequiredService<UserCreator>();
    userCreator.Run(options.Username, options.Email, options.Password);
}

Parser.Default
    .ParseArguments<CliOptions>(args)
    .WithParsed(options => Run(options));
