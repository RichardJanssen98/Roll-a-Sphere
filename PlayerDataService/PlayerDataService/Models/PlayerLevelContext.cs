using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlayerDataService.Models;

namespace PlayerDataService.Models
{
    public class PlayerLevelContext : DbContext
    {
        public PlayerLevelContext(DbContextOptions<PlayerLevelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PlayerLevel> PlayerLevels { get; set; }

        public virtual DbSet<PlayerLocation> PlayerLocations { get; set; }
    }
}
