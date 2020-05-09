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
    }
}
