using System;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleQuotes.Models;
using VehicleQuotes.ResourceModels;
using VehicleQuotes.Services;
// using Microsoft.Extensions.Logging;

namespace VehicleQuotes.Tests
{
    public class QuoteServiceTests
    {
        private IConfiguration configuration;

        public QuoteServiceTests()
        {
            var host = Host.CreateDefaultBuilder().Build();
            configuration = host.Services.GetRequiredService<IConfiguration>();
        }

        private VehicleQuotesContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<VehicleQuotesContext>()
                .UseNpgsql(configuration.GetConnectionString("VehicleQuotesContext"))
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

        [Fact]
        public async void GetAllQuotesReturnsEmptyWhenThereIsNoDataStored()
        {
            // Given
            var dbContext = CreateDbContext();
            var service = new QuoteService(dbContext, null);

            // When
            var result = await service.GetAllQuotes();

            // Then
            Assert.Empty(result);
        }

        [Fact]
        public async void GetAllQuotesReturnsTheStoredData()
        {
            // Given
            var dbContext = CreateDbContext();

            var quote = new Quote
            {
                OfferedQuote = 100,
                Message = "test_quote_message",

                Year = "2000",
                Make = "Toyota",
                Model = "Corolla",
                BodyTypeID = dbContext.BodyTypes.Single(bt => bt.Name == "Sedan").ID,
                SizeID = dbContext.Sizes.Single(s => s.Name == "Compact").ID,

                ItMoves = true,
                HasAllWheels = true,
                HasAlloyWheels = true,
                HasAllTires = true,
                HasKey = true,
                HasTitle = true,
                RequiresPickup = true,
                HasEngine = true,
                HasTransmission = true,
                HasCompleteInterior = true,

                CreatedAt = DateTime.Now
            };

            dbContext.Quotes.Add(quote);

            dbContext.SaveChanges();

            var service = new QuoteService(dbContext, null);

            // When
            var result = await service.GetAllQuotes();

            // Then
            Assert.NotEmpty(result);
            Assert.Single(result);
            Assert.Equal(quote.ID, result.First().ID);
            Assert.Equal(quote.OfferedQuote, result.First().OfferedQuote);
            Assert.Equal(quote.Message, result.First().Message);
        }
    
        [Fact]
        public async void CalculateQuoteStoresANewQuoteRecord()
        {
            // Given
            var dbContext = CreateDbContext();
            var service = new QuoteService(dbContext, configuration);

            var quoteRequest = new QuoteRequest
            {
                Year = "2000",
                Make = "Toyota",
                Model = "Corolla",
                BodyType = "Sedan",
                Size = "Compact",
                ItMoves = true,
                HasAllWheels = true,
                HasAlloyWheels = true,
                HasAllTires = true,
                HasKey = true,
                HasTitle = true,
                RequiresPickup = true,
                HasEngine = true,
                HasTransmission = true,
                HasCompleteInterior = true
            };

            // When
            var result = await service.CalculateQuote(quoteRequest);

            // Then
            Assert.Single(dbContext.Quotes);
            Assert.Equal(result.ID, dbContext.Quotes.First().ID);
            Assert.Equal(result.CreatedAt, dbContext.Quotes.First().CreatedAt);
            Assert.Equal(result.OfferedQuote, dbContext.Quotes.First().OfferedQuote);
            Assert.Equal(result.Message, dbContext.Quotes.First().Message);
        }
    }
}
