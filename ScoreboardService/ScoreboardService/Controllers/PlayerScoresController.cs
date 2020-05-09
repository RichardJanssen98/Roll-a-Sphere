using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreboardService.Models;

namespace ScoreboardService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerScoresController : ControllerBase
    {
        private readonly PlayerScoreContext _context;

        public PlayerScoresController(PlayerScoreContext context)
        {
            _context = context;
        }

        // GET: api/PlayerScores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerScore>>> GetPlayerScores()
        {
            return await _context.PlayerScores.ToListAsync();
        }

        // GET: api/PlayerScores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerScore>> GetPlayerScore(int id)
        {
            var playerScore = await _context.PlayerScores.FindAsync(id);

            if (playerScore == null)
            {
                return NotFound();
            }

            return playerScore;
        }

        // PUT: api/PlayerScores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerScore(int id, PlayerScore playerScore)
        {
            if (id != playerScore.PlayerScoreId)
            {
                return BadRequest();
            }

            _context.Entry(playerScore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerScoreExists(id))
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

        // POST: api/PlayerScores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlayerScore>> PostPlayerScore(PlayerScore playerScore)
        {
            _context.PlayerScores.Add(playerScore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerScore", new { id = playerScore.PlayerScoreId }, playerScore);
        }

        // DELETE: api/PlayerScores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlayerScore>> DeletePlayerScore(int id)
        {
            var playerScore = await _context.PlayerScores.FindAsync(id);
            if (playerScore == null)
            {
                return NotFound();
            }

            _context.PlayerScores.Remove(playerScore);
            await _context.SaveChangesAsync();

            return playerScore;
        }

        private bool PlayerScoreExists(int id)
        {
            return _context.PlayerScores.Any(e => e.PlayerScoreId == id);
        }
    }
}
