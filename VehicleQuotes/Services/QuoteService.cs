using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VehicleQuotes.Models;
using VehicleQuotes.ResourceModels;

namespace VehicleQuotes.Services
{
    public class QuoteService
    {
        private readonly VehicleQuotesContext _context;
        private readonly IConfiguration _configuration;

        public QuoteService(VehicleQuotesContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<SubmittedQuoteRequest>> GetAllQuotes()
        {
            var quotesToReturn = _context.Quotes.Select(q => new SubmittedQuoteRequest
            {
                ID = q.ID,
                CreatedAt = q.CreatedAt,
                OfferedQuote = q.OfferedQuote,
                Message = q.Message,

                Year = q.Year,
                Make = q.Make,
                Model = q.Model,
                BodyType = q.BodyType.Name,
                Size = q.Size.Name,

                ItMoves = q.ItMoves,
                HasAllWheels = q.HasAllWheels,
                HasAlloyWheels = q.HasAlloyWheels,
                HasAllTires = q.HasAllTires,
                HasKey = q.HasKey,
                HasTitle = q.HasTitle,
                RequiresPickup = q.RequiresPickup,
                HasEngine = q.HasEngine,
                HasTransmission = q.HasTransmission,
                HasCompleteInterior = q.HasCompleteInterior,
            });

            return await quotesToReturn.ToListAsync();
        }

        public async Task<SubmittedQuoteRequest> CalculateQuote(QuoteRequest request)
        {
            var response = this.CreateResponse(request);
            var quoteToStore = await this.CreateQuote(request);
            var requestedModelStyleYear = await this.FindModelStyleYear(request);
            QuoteOverride quoteOverride = null;

            if (requestedModelStyleYear != null)
            {
                quoteToStore.ModelStyleYear = requestedModelStyleYear;

                quoteOverride = await this.FindQuoteOverride(requestedModelStyleYear);

                if (quoteOverride != null)
                {
                    response.OfferedQuote = quoteOverride.Price;
                }
            }

            if (quoteOverride == null)
            {
                response.OfferedQuote = await this.CalculateOfferedQuote(request);
            }

            if (requestedModelStyleYear == null)
            {
                response.Message = "Offer subject to change upon vehicle inspection.";
            }

            if (response.OfferedQuote <= 0)
            {
                response.OfferedQuote = _configuration.GetValue<int>("DefaultOffer", 0);
            }

            quoteToStore.OfferedQuote = response.OfferedQuote;
            quoteToStore.Message = response.Message;

            _context.Quotes.Add(quoteToStore);
            await _context.SaveChangesAsync();

            response.ID = quoteToStore.ID;
            response.CreatedAt = quoteToStore.CreatedAt;

            return response;
        }

        private SubmittedQuoteRequest CreateResponse(QuoteRequest request)
        {
            return new SubmittedQuoteRequest
            {
                OfferedQuote = 0,
                Message = "This is our final offer.",

                Year = request.Year,
                Make = request.Make,
                Model = request.Model,
                BodyType = request.BodyType,
                Size = request.Size,

                ItMoves = request.ItMoves,
                HasAllWheels = request.HasAllWheels,
                HasAlloyWheels = request.HasAlloyWheels,
                HasAllTires = request.HasAllTires,
                HasKey = request.HasKey,
                HasTitle = request.HasTitle,
                RequiresPickup = request.RequiresPickup,
                HasEngine = request.HasEngine,
                HasTransmission = request.HasTransmission,
                HasCompleteInterior = request.HasCompleteInterior,
            };
        }

        private async Task<Quote> CreateQuote(QuoteRequest request)
        {
            return new Quote
            {
                Year = request.Year,
                Make = request.Make,
                Model = request.Model,
                BodyTypeID = (await _context.BodyTypes.SingleAsync(bt => bt.Name == request.BodyType)).ID,
                SizeID = (await _context.Sizes.SingleAsync(s => s.Name == request.Size)).ID,

                ItMoves = request.ItMoves,
                HasAllWheels = request.HasAllWheels,
                HasAlloyWheels = request.HasAlloyWheels,
                HasAllTires = request.HasAllTires,
                HasKey = request.HasKey,
                HasTitle = request.HasTitle,
                RequiresPickup = request.RequiresPickup,
                HasEngine = request.HasEngine,
                HasTransmission = request.HasTransmission,
                HasCompleteInterior = request.HasCompleteInterior,

                CreatedAt = DateTime.Now
            };
        }

        private async Task<ModelStyleYear> FindModelStyleYear(QuoteRequest request)
        {
            return await _context.ModelStyleYears.FirstOrDefaultAsync(msy =>
                msy.Year == request.Year &&
                msy.ModelStyle.Model.Make.Name == request.Make &&
                msy.ModelStyle.Model.Name == request.Model &&
                msy.ModelStyle.BodyType.Name == request.BodyType &&
                msy.ModelStyle.Size.Name == request.Size
            );
        }

        private async Task<QuoteOverride> FindQuoteOverride(ModelStyleYear modelStyleYear)
        {
            return await _context.QuoteOverides
                .FirstOrDefaultAsync(qo => qo.ModelStyleYear == modelStyleYear);
        }

        private async Task<int> CalculateOfferedQuote(QuoteRequest request)
        {
            var rules = await _context.QuoteRules.ToListAsync();

            Func<string, QuoteRule> theMatchingRule = featureType =>
                rules.FirstOrDefault(r =>
                    r.FeatureType == featureType &&
                    r.FeatureValue == request[featureType]
                );

            return QuoteRule.FeatureTypes.All
                .Select(theMatchingRule)
                .Where(r => r != null)
                .Sum(r => r.PriceModifier);
        }
    }
}