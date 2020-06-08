using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerDataService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PlayerDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerLevelsController : ControllerBase
    {
        private readonly PlayerLevelContext _context;

        private ConnectionFactory factory;
        private IConnection conn;
        private IModel channel;

        public PlayerLevelsController(PlayerLevelContext context)
        {
            _context = context;

            factory = new ConnectionFactory { HostName = "localhost" };
            conn = factory.CreateConnection();
            channel = conn.CreateModel();

            channel.QueueDeclare(
                queue: "AccountGhostQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                Console.WriteLine($"[MessageQueue] Received message '{message}'");

                var messageParts = message.Split(":");

                var result = 0;
                if (messageParts[0] == "DELETE")
                {
                    result = await DeleteRecordsOfPlayerId(Int32.Parse(messageParts[1]));
                }
                else if (messageParts[0] == "CHANGE")
                {
                    result = await ChangeUserNameOfPlayerId(Int32.Parse(messageParts[1]), messageParts[2]);
                }

                Console.WriteLine($"[MessageQueue] Processed message '{message}' and touched {result} records.");
            };

            channel.BasicConsume(queue: "AccountGhostQueue", autoAck: true, consumer: consumer);
        }

        //Destructor
        ~PlayerLevelsController()
        {
            //Connection and Channels are meant to be long-lived
            //So we don't open and close them for each operation

            channel.Close();
            conn.Close();
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
        private async Task<int> DeleteRecordsOfPlayerId(int playerId)
        {
            var deletedAmount = 0;

            var optionsBuilder = new DbContextOptionsBuilder<PlayerLevelContext>();
            optionsBuilder.UseInMemoryDatabase("PlayerDataDB");
            using (var tempContext = new PlayerLevelContext(optionsBuilder.Options))
            {
                var playerHighScores = await tempContext.PlayerLevels.Where(h => h.PlayerAccountId == playerId).ToListAsync();

                deletedAmount = playerHighScores.Count;

                if (deletedAmount == 0) { return 0; }

                tempContext.PlayerLevels.RemoveRange(playerHighScores);
                await tempContext.SaveChangesAsync();
            }

            return deletedAmount;
        }

        private async Task<int> ChangeUserNameOfPlayerId(int playerId, string userName)
        {
            var changedAmount = 0;

            var optionsBuilder = new DbContextOptionsBuilder<PlayerLevelContext>();
            optionsBuilder.UseInMemoryDatabase("PlayerDataDB");
            using (var tempContext = new PlayerLevelContext(optionsBuilder.Options))
            {
                var playerHighScores = await tempContext.PlayerLevels.Where(h => h.PlayerAccountId == playerId).Include(h => h.PlayerLocations).ToListAsync();

                changedAmount = playerHighScores.Count;

                if (changedAmount == 0) { return 0; }

                foreach (PlayerLevel playerLevel in playerHighScores)
                {
                    PlayerLevel playerLevelNewUsername = new PlayerLevel(playerLevel.PlayerLevelId, playerLevel.PlayerAccountId, playerLevel.Level, playerLevel.EmailPlayer, userName, playerLevel.PlayerLocations);
                    tempContext.Entry(playerLevel).State = EntityState.Detached;

                    tempContext.Entry(playerLevelNewUsername).State = EntityState.Modified;
                }

                await tempContext.SaveChangesAsync();
            }

            return changedAmount;
        }
    }
}
