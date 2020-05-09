using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreboardService.Models
{
    public class PlayerScore
    {
        public int PlayerScoreId { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
        public string EmailPlayer { get; set; }
        public string Username { get; set; }
    }
}
