using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Logging;

namespace VehicleQuotes.Tests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public IConfiguration Configuration { get; private set; }
        public VehicleQuotesContext DbContext { get; private set; }

        public DatabaseFixture()
        {
            Configuration = LoadConfiguration();
            DbContext = CreateDbContext();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        private IConfiguration LoadConfiguration()
        {
            var host = Host.CreateDefaultBuilder().Build();
            return host.Services.GetRequiredService<IConfiguration>();
        }

        private VehicleQuotesContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<VehicleQuotesContext>()
                .UseNpgsql(Configuration.GetConnectionString("VehicleQuotesContext"))
                // .UseNpgsql("Host=db;Database=vehicle_quotes_test;Username=vehicle_quotes;Password=password")
                .UseSnakeCaseNamingConvention()
                // .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                // .EnableSensitiveDataLogging()
                .Options;

            var context = new VehicleQuotesContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
