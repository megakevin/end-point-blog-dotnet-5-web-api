using System;

namespace VehicleQuotes.ResourceModels
{
    public class SubmittedQuoteRequest : QuoteRequest
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OfferedQuote { get; set; }
        public string Message { get; internal set; }
    }
}
