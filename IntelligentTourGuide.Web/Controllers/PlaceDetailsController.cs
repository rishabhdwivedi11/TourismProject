using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntelligentTourGuide.Web.Data;
using IntelligentTourGuide.Web.Models;

namespace IntelligentTourGuide.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlaceDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PlaceDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceDetail>>> GetPlaceDetails()
        {
            return await _context.PlaceDetails.ToListAsync();
        }

        // GET: api/PlaceDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceDetail>> GetPlaceDetail(int id)
        {
            var placeDetail = await _context.PlaceDetails.FindAsync(id);

            if (placeDetail == null)
            {
                return NotFound();
            }

            return placeDetail;
        }

        // PUT: api/PlaceDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaceDetail(int id, PlaceDetail placeDetail)
        {
            if (id != placeDetail.PlaceDetailId)
            {
                return BadRequest();
            }

            _context.Entry(placeDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceDetailExists(id))
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

        // POST: api/PlaceDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlaceDetail>> PostPlaceDetail(PlaceDetail placeDetail)
        {
            _context.PlaceDetails.Add(placeDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaceDetail", new { id = placeDetail.PlaceDetailId }, placeDetail);
        }

        // DELETE: api/PlaceDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlaceDetail>> DeletePlaceDetail(int id)
        {
            var placeDetail = await _context.PlaceDetails.FindAsync(id);
            if (placeDetail == null)
            {
                return NotFound();
            }

            _context.PlaceDetails.Remove(placeDetail);
            await _context.SaveChangesAsync();

            return placeDetail;
        }

        private bool PlaceDetailExists(int id)
        {
            return _context.PlaceDetails.Any(e => e.PlaceDetailId == id);
        }
    }
}
