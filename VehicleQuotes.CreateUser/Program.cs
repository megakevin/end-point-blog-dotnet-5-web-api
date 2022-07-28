using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehicleQuotes.CreateUser;

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
userCreator.Run(args[0], args[1], args[2]);

