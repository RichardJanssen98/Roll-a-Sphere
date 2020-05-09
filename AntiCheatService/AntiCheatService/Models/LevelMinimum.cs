using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntiCheatService.Models
{
    public class LevelMinimum
    {
        public int LevelMinimumId { get; set; }    
        public int MaxScore { get; set; }
        public int MinimumTime { get; set; }
    }
}
