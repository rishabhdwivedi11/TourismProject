using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntelligentTourGuide.Web.Data;
using IntelligentTourGuide.Web.Models;
using Microsoft.Extensions.Logging;

namespace IntelligentTourGuide.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatesController> _logger;

        public StatesController(ApplicationDbContext context, ILogger<StatesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/States
        [HttpGet]
        public async Task<IActionResult> GetStates()
        {
            try
            {
                var states = await _context.States.ToListAsync();

                if (states == null)
                {
                    _logger.LogWarning("No States were found in the database");
                    return NotFound();
                }
                _logger.LogInformation("Extracted all the States from database");
                return Ok(states);
            }
            catch
            {
                _logger.LogError("There was an attempt to retrieve information from the database");
                return BadRequest();
            }
        }

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetState(short? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                var state = await _context.States.FindAsync(id);
                //var state = await _context.States
                //                             .Include(c => c.Place)
                //                             .SingleOrDefaultAsync(c => c.StateId == id);

                if (state == null)
                {
                    return NotFound();
                }

                return Ok(state);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/States/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutState(short id, State state)
        {
            if (id != state.StateId)
            {
                return BadRequest();
            }

            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/States
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostState(State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.States.Add(state);

                int countAffected = await _context.SaveChangesAsync();
                if (countAffected > 0)
                {
                    // Return the link to the newly inserted row 
                    var result = CreatedAtAction("GetState", new { id = state.StateId }, state);
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("Post", exp.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/States/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(short? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                var state = await _context.States.FindAsync(id);
                if (state == null)
                {
                    return NotFound();
                }

                _context.States.Remove(state);
                await _context.SaveChangesAsync();

                return Ok(state);
            }
            catch
            {
                return BadRequest();
            }
        }

        private bool StateExists(short id)
        {
            return _context.States.Any(e => e.StateId == id);
        }
    }
}
