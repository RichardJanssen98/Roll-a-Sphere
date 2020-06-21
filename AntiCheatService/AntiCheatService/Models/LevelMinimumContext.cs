using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntiCheatService.Models
{
    public class LevelMinimumContext : DbContext
    {
        public LevelMinimumContext(DbContextOptions<LevelMinimumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LevelMinimum> LevelMinimums { get; set; }


        public static bool TestForCheat(LevelMinimum levelMinimum, int level, int score, int time)
        {
            if (level == levelMinimum.LevelMinimumId && (score <= levelMinimum.MaxScore && time >= levelMinimum.MinimumTime))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
