using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AntiCheatService.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace AntiCheatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelMinimumsController : ControllerBase
    {
        private readonly LevelMinimumContext _context;

        public LevelMinimumsController(LevelMinimumContext context)
        {
            _context = context;
        }

        // GET: api/LevelMinimums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LevelMinimum>>> GetLevelMinimums()
        {
            return await _context.LevelMinimums.ToListAsync();
        }

        // GET: api/LevelMinimums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LevelMinimum>> GetLevelMinimum(int id)
        {
            var levelMinimum = await _context.LevelMinimums.FindAsync(id);

            if (levelMinimum == null)
            {
                return NotFound();
            }

            return levelMinimum;
        }

        // PUT: api/LevelMinimums/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLevelMinimum(int id, LevelMinimum levelMinimum)
        {
            if (id != levelMinimum.LevelMinimumId)
            {
                return BadRequest();
            }

            _context.Entry(levelMinimum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelMinimumExists(id))
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

        // POST: api/levelminimums/amicheating?level=a&score=b&time=c
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("amicheating")]
        public async Task<bool> PostTestCheating(int level, int score, int time)
        {
            var validation = await _context.LevelMinimums.FirstOrDefaultAsync(l => l.LevelMinimumId == level);
            bool result = LevelMinimumContext.TestForCheat(validation, level, score, time);

            return result;
        }

        // POST: api/LevelMinimums
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LevelMinimum>> PostLevelMinimum(LevelMinimum levelMinimum)
        {
            _context.LevelMinimums.Add(levelMinimum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLevelMinimum", new { id = levelMinimum.LevelMinimumId }, levelMinimum);
        }

        // DELETE: api/LevelMinimums/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LevelMinimum>> DeleteLevelMinimum(int id)
        {
            var levelMinimum = await _context.LevelMinimums.FindAsync(id);
            if (levelMinimum == null)
            {
                return NotFound();
            }

            _context.LevelMinimums.Remove(levelMinimum);
            await _context.SaveChangesAsync();

            return levelMinimum;
        }

        private bool LevelMinimumExists(int id)
        {
            return _context.LevelMinimums.Any(e => e.LevelMinimumId == id);
        }
    }
}
