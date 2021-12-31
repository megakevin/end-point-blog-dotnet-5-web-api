using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleQuotes.Models;
using VehicleQuotes.ResourceModels;

namespace VehicleQuotes.Controllers
{
    [Route("api/Make/{makeId}/[controller]/")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly VehicleQuotesContext _context;

        public ModelsController(VehicleQuotesContext context)
        {
            _context = context;
        }

        // GET: api/Make/{makeId}/Models
        /// <summary>
        /// Gets all the available vehicle models for the given make ID.
        /// </summary>
        /// <param name="makeId">The ID of the vehicle make to get models from.</param>
        /// <response code="200">Returns a collection of all the registered vehicle models for the given make.</response>
        /// <response code="404">When the specified vehicle make does not exist.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModelSpecification>>> GetAll([FromRoute] int makeId)
        {
            var make = await _context.Makes.FindAsync(makeId);

            if (make == null)
            {
                return NotFound();
            }

            var modelsToReturn = _context.Models
                .Where(m => m.MakeID == makeId)
                .Select(m => new ModelSpecification {
                    ID = m.ID,
                    Name = m.Name,
                    Styles = m.ModelStyles.Select(ms => new ModelSpecificationStyle {
                        BodyType = ms.BodyType.Name,
                        Size = ms.Size.Name,
                        Years = ms.ModelStyleYears.Select(msy => msy.Year).ToArray()
                    }).ToArray()
                });

            return await modelsToReturn.ToListAsync();
        }

        // GET: api/Make/{makeId}/Models/5
        /// <summary>
        /// Gets a vehicle model specified by the given make and model IDs.
        /// </summary>
        /// <param name="makeId">The ID of the vehicle make to get models from.</param>
        /// <param name="id">The ID of the vehicle model to get.</param>
        /// <response code="200">Returns a vehicle model identified by the given make and model IDs.</response>
        /// <response code="404">When the specified vehicle model does not exist.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModelSpecification>> GetOne([FromRoute] int makeId, [FromRoute] int id)
        {
            var model = await _context.Models
                .Include(m => m.ModelStyles).ThenInclude(ms => ms.BodyType)
                .Include(m => m.ModelStyles).ThenInclude(ms => ms.Size)
                .Include(m => m.ModelStyles).ThenInclude(ms => ms.ModelStyleYears)
                .FirstOrDefaultAsync(m => m.MakeID == makeId && m.ID == id);

            if (model == null)
            {
                return NotFound();
            }

            return new ModelSpecification {
                ID = model.ID,
                Name = model.Name,
                Styles = model.ModelStyles.Select(ms => new ModelSpecificationStyle {
                    BodyType = ms.BodyType.Name,
                    Size = ms.Size.Name,
                    Years = ms.ModelStyleYears.Select(msy => msy.Year).ToArray()
                }).ToArray()
            };
        }

        // PUT: api/Make/{makeId}/Models/5
        /// <summary>
        /// Updates a vehicle model specified by the given make and model IDs.
        /// </summary>
        /// <param name="makeId">The ID of the vehicle make to get models from.</param>
        /// <param name="id">The ID of the vehicle model to update.</param>
        /// <param name="model">The data to update the model to.</param>
        /// <response code="400">When the request is invalid.</response>
        /// <response code="404">When the specified vehicle model does not exist.</response>
        /// <response code="409">When there's already another model in the same make with the same name.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Put([FromRoute] int makeId, int id, ModelSpecification model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }

            var modelToUpdate = await _context.Models
                // We need this, if not, the new styles are added instead of substituting the old ones
                .Include(m => m.ModelStyles)
                .FirstOrDefaultAsync(m => m.MakeID == makeId && m.ID == id);

            if (modelToUpdate == null)
            {
                return NotFound();
            }

            modelToUpdate.Name = model.Name;

            modelToUpdate.ModelStyles = model.Styles.Select(style => new ModelStyle {
                BodyType = _context.BodyTypes.Single(bodyType => bodyType.Name == style.BodyType),
                Size = _context.Sizes.Single(size => size.Name == style.Size),

                ModelStyleYears = style.Years.Select(year => new ModelStyleYear {
                    Year = year
                }).ToList()
            }).ToList();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return Conflict();
            }

            return NoContent();
        }

        // POST: api/Make/{makeId}/Models
        /// <summary>
        /// Creates a new vehicle model for the given make.
        /// </summary>
        /// <param name="makeId">The ID of the vehicle make to add the model to.</param>
        /// <param name="model">The data to create the new model with.</param>
        /// <response code="201">When the request is invalid.</response>
        /// <response code="404">When the specified vehicle make does not exist.</response>
        /// <response code="409">When there's already another model in the same make with the same name.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ModelSpecification>> Post([FromRoute] int makeId, ModelSpecification model)
        {
            var make = await _context.Makes.FindAsync(makeId);

            if (make == null)
            {
                return NotFound();
            }

            var modelToCreate = new Model {
                Make = make,
                Name = model.Name,

                ModelStyles = model.Styles.Select(style => new ModelStyle {
                    BodyType = _context.BodyTypes.Single(bodyType => bodyType.Name == style.BodyType),
                    Size = _context.Sizes.Single(size => size.Name == style.Size),

                    ModelStyleYears = style.Years.Select(year => new ModelStyleYear {
                        Year = year
                    }).ToArray()
                }).ToArray()
            };

            _context.Add(modelToCreate);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return Conflict();
            }

            model.ID = modelToCreate.ID;

            return CreatedAtAction(
                nameof(GetOne),
                new { makeId = makeId, id = model.ID },
                model
            );
        }

        // DELETE: api/Make/{makeId}/Models/5
        /// <summary>
        /// Deletes a vehicle model specified by the given make and model IDs.
        /// </summary>
        /// <param name="makeId">The ID of the vehicle make to delete the model from.</param>
        /// <param name="id">The ID of the vehicle model to delete.</param>
        /// <response code="404">When the specified vehicle model does not exist.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int makeId, int id)
        {
            var model = await _context.Models.FirstOrDefaultAsync(m => m.MakeID == makeId && m.ID == id);

            if (model == null)
            {
                return NotFound();
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
