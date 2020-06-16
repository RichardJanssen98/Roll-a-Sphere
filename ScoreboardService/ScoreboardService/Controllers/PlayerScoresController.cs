using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ScoreboardService.Models;
using Microsoft.Extensions.Configuration;

namespace ScoreboardService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerScoresController : ControllerBase
    {
        private readonly PlayerScoreContext _context;

        private ConnectionFactory factory;
        private IConnection conn;
        private IModel channel;

        private readonly IConfiguration configuration;

        public PlayerScoresController(PlayerScoreContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            _context = context;

            var rabbitMQOption = configuration.GetSection(RabbitMQOptions.Position)
                .Get<RabbitMQOptions>();

            factory = new ConnectionFactory { HostName = rabbitMQOption.Connection };
            conn = factory.CreateConnection();
            channel = conn.CreateModel();

            channel.QueueDeclare(
                queue: "AccountQueue",
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

            channel.BasicConsume(queue: "AccountQueue", autoAck: true, consumer: consumer);
        }

        //Destructor
        ~PlayerScoresController()
        {
            //Connection and Channels are meant to be long-lived
            //So we don't open and close them for each operation

            channel.Close();
            conn.Close();
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

        // POST: api/playerScores/playerScore?playeraccountid=x&level=a&score=b&time=c&emailPlayer=d&userName=e
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("playerScore")]
        public async Task<ActionResult<PlayerScore>> PostPlayerScore(int playerAccountId, int level, int score, int time, string emailPlayer, string userName)
        {
            PlayerScore playerScore = new PlayerScore(playerAccountId, level, score, time, emailPlayer, userName);
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

        private async Task<int> DeleteRecordsOfPlayerId(int playerId)
        {
            var deletedAmount = 0;

            var optionsBuilder = new DbContextOptionsBuilder<PlayerScoreContext>();
            optionsBuilder.UseInMemoryDatabase("PlayerScoreDB");
            using (var tempContext = new PlayerScoreContext(optionsBuilder.Options))
            {
                var playerHighScores = await tempContext.PlayerScores.Where(h => h.PlayerAccountId == playerId).ToListAsync();

                deletedAmount = playerHighScores.Count;

                if (deletedAmount == 0) { return 0; }

                tempContext.PlayerScores.RemoveRange(playerHighScores);
                await tempContext.SaveChangesAsync();
            }

            return deletedAmount;
        }

        private async Task<int> ChangeUserNameOfPlayerId(int playerId, string userName)
        {
            var changedAmount = 0;

            var optionsBuilder = new DbContextOptionsBuilder<PlayerScoreContext>();
            optionsBuilder.UseInMemoryDatabase("PlayerScoreDB");
            using (var tempContext = new PlayerScoreContext(optionsBuilder.Options))
            {
                var playerHighScores = await tempContext.PlayerScores.Where(h => h.PlayerAccountId == playerId).ToListAsync();

                changedAmount = playerHighScores.Count;

                if (changedAmount == 0) { return 0; }

                foreach (PlayerScore playerScore in playerHighScores)
                {
                    PlayerScore playerScoreNewUsername = new PlayerScore(playerScore.PlayerScoreId, playerScore.PlayerAccountId, playerScore.Level, playerScore.Score, playerScore.Time, playerScore.EmailPlayer, userName);
                    tempContext.Entry(playerScore).State = EntityState.Detached;

                    tempContext.Entry(playerScoreNewUsername).State = EntityState.Modified;
                }

                await tempContext.SaveChangesAsync();
            }

            return changedAmount;
        }
    }
}
