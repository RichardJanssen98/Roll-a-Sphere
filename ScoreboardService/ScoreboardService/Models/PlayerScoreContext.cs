using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreboardService.Models
{
    public class PlayerScoreContext : DbContext
    {
        public PlayerScoreContext(DbContextOptions<PlayerScoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PlayerScore> PlayerScores { get; set; }
    }
}
