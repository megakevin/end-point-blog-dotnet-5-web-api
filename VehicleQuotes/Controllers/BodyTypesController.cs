using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleQuotes;
using VehicleQuotes.Models;

namespace VehicleQuotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyTypesController : ControllerBase
    {
        private readonly VehicleQuotesContext _context;

        public BodyTypesController(VehicleQuotesContext context)
        {
            _context = context;
        }

        // GET: api/BodyTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodyType>>> GetBodyTypes()
        {
            return await _context.BodyTypes.ToListAsync();
        }
    }
}
