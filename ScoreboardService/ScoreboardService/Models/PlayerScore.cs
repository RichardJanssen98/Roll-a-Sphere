using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreboardService.Models
{
    public class PlayerScore
    { 

        public int PlayerScoreId { get; set; }
        public int PlayerAccountId { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
        public string EmailPlayer { get; set; }
        public string Username { get; set; }

        public PlayerScore(int playerAccountId, int level, int score, int time, string emailPlayer, string userName)
        {
            this.PlayerAccountId = playerAccountId;
            this.Level = level;
            this.Score = score;
            this.Time = time;
            this.EmailPlayer = emailPlayer;
            this.Username = userName;
        }

        public PlayerScore(int playerScoreId, int playerAccountId, int level, int score, int time, string emailPlayer, string userName)
        {
            this.PlayerScoreId = playerScoreId;
            this.PlayerAccountId = playerAccountId;
            this.Level = level;
            this.Score = score;
            this.Time = time;
            this.EmailPlayer = emailPlayer;
            this.Username = userName;
        }

        public PlayerScore()
        {

        }
    }
}
