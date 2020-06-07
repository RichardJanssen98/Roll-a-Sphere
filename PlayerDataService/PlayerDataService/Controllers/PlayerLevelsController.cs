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
    public class PlayerLevelsController : ControllerBase
    {
        private readonly PlayerLevelContext _context;

        public PlayerLevelsController(PlayerLevelContext context)
        {
            _context = context;
        }

        // GET: api/PlayerLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerLevel>>> GetPlayerLevels()
        {
            return await _context.PlayerLevels.Include(playerLevel => playerLevel.PlayerLocations).ToListAsync();
        }

        // GET: api/PlayerLevels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerLevel>> GetPlayerLevel(int id)
        {
            var playerLevel = await _context.PlayerLevels.FindAsync(id);

            if (playerLevel == null)
            {
                return NotFound();
            }

            return playerLevel;
        }

        // PUT: api/PlayerLevels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerLevel(int id, PlayerLevel playerLevel)
        {
            if (id != playerLevel.PlayerLevelId)
            {
                return BadRequest();
            }

            _context.Entry(playerLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerLevelExists(id))
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

        // POST: api/PlayerLevels/postLocations?level=a&emailPlayer=b&userName=c
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("postLocations")]
        public async Task<ActionResult<PlayerLevel>> PostPlayerLevel(PlayerLevel playerLevel)
        {
            _context.PlayerLevels.Add(playerLevel);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerLevel", new { id = playerLevel.PlayerLevelId }, playerLevel);
        }

        // DELETE: api/PlayerLevels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlayerLevel>> DeletePlayerLevel(int id)
        {
            var playerLevel = await _context.PlayerLevels.FindAsync(id);
            if (playerLevel == null)
            {
                return NotFound();
            }

            _context.PlayerLevels.Remove(playerLevel);
            await _context.SaveChangesAsync();

            return playerLevel;
        }

        private bool PlayerLevelExists(int id)
        {
            return _context.PlayerLevels.Any(e => e.PlayerLevelId == id);
        }
    }
}
