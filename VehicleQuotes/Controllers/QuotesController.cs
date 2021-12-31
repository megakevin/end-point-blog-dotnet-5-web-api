using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleQuotes.ResourceModels;
using VehicleQuotes.Services;

namespace VehicleQuotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly QuoteService _service;

        public QuotesController(QuoteService service)
        {
            _service = service;
        }

        // GET: api/Quotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubmittedQuoteRequest>>> GetAll()
        {
            return await _service.GetAllQuotes();
        }

        // POST: api/Quotes
        [HttpPost]
        public async Task<ActionResult<SubmittedQuoteRequest>> Post(QuoteRequest request)
        {
            return await _service.CalculateQuote(request);
        }
    }
}
