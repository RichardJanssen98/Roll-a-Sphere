using AntiCheatService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntiCheatService.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LevelMinimumContext(
                serviceProvider.GetRequiredService<DbContextOptions<LevelMinimumContext>>()))
            {
                // Look for any board games.
                if (context.LevelMinimums.Any())
                {
                    return;   // Data was already seeded
                }

                context.LevelMinimums.AddRange(
                    new LevelMinimum
                    {
                        LevelMinimumId = 1,
                        MaxScore = 320,
                        MinimumTime = 16
                    });
                context.SaveChanges();
            }
        }
    }
}
