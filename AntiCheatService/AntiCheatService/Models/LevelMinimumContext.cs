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

        
        public bool TestForCheat(int level, int score, int time)
        {
            LevelMinimum levelMinimum = LevelMinimums.Find(level);
            
            if (levelMinimum != null)
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
            else
            {
                return true;
            }
        }
    }
}
