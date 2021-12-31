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
    public class SizesController : ControllerBase
    {
        private readonly VehicleQuotesContext _context;

        public SizesController(VehicleQuotesContext context)
        {
            _context = context;
        }

        // GET: api/Sizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Size>>> GetSizes()
        {
            return await _context.Sizes.ToListAsync();
        }
    }
}
