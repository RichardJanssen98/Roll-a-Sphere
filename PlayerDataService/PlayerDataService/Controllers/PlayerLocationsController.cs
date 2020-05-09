using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerDataService.Models;

namespace PlayerDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerLocationsController : ControllerBase
    {
        private readonly PlayerLevelContext _context;

        public PlayerLocationsController(PlayerLevelContext context)
        {
            _context = context;
        }

        // GET: api/PlayerLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerLocation>>> GetPlayerLocations()
        {
            return await _context.PlayerLocations.ToListAsync();
        }

        // GET: api/PlayerLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerLocation>> GetPlayerLocation(int id)
        {
            var playerLocation = await _context.PlayerLocations.FindAsync(id);

            if (playerLocation == null)
            {
                return NotFound();
            }

            return playerLocation;
        }

        // PUT: api/PlayerLocations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerLocation(int id, PlayerLocation playerLocation)
        {
            if (id != playerLocation.PlayerLocationId)
            {
                return BadRequest();
            }

            _context.Entry(playerLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerLocationExists(id))
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

        // POST: api/PlayerLocations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlayerLocation>> PostPlayerLocation(PlayerLocation playerLocation)
        {
            _context.PlayerLocations.Add(playerLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerLocation", new { id = playerLocation.PlayerLocationId }, playerLocation);
        }

        // DELETE: api/PlayerLocations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlayerLocation>> DeletePlayerLocation(int id)
        {
            var playerLocation = await _context.PlayerLocations.FindAsync(id);
            if (playerLocation == null)
            {
                return NotFound();
            }

            _context.PlayerLocations.Remove(playerLocation);
            await _context.SaveChangesAsync();

            return playerLocation;
        }

        private bool PlayerLocationExists(int id)
        {
            return _context.PlayerLocations.Any(e => e.PlayerLocationId == id);
        }
    }
}
