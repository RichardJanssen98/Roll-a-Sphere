using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerDataService.Models
{
    [Serializable]
    public class PlayerLevel
    {
        public int PlayerLevelId { get; set; }
        public int PlayerAccountId { get; set; }
        public int Level { get; set; }
        public string EmailPlayer { get; set; }
        public string Username { get; set; }

        public List<PlayerLocation> PlayerLocations { get; set; }

        public PlayerLevel(int playerAccountId, int level, string emailPlayer, string userName, List<PlayerLocation> playerLocations)
        {
            this.PlayerAccountId = playerAccountId;
            this.Level = level;
            this.EmailPlayer = emailPlayer;
            this.Username = userName;
            this.PlayerLocations = new List<PlayerLocation>();

            foreach (PlayerLocation playerLoc in playerLocations)
            {
                PlayerLocation playerLocationToAdd = new PlayerLocation(playerLoc.X, playerLoc.Y, playerLoc.Z, playerLoc.LocationNumber);

                this.PlayerLocations.Add(playerLocationToAdd);
                Console.WriteLine(playerLoc.X + " " + playerLoc.Y + " " + playerLoc.Z + " " + playerLoc.PlayerLocationId + " " + playerLoc.LocationNumber);
            }
        }

        public PlayerLevel(int playerLevelId, int playerAccountId, int level, string emailPlayer, string userName, List<PlayerLocation> playerLocations)
        {
            this.PlayerLevelId = playerLevelId;
            this.PlayerAccountId = playerAccountId;
            this.Level = level;
            this.EmailPlayer = emailPlayer;
            this.Username = userName;
            this.PlayerLocations = new List<PlayerLocation>();

            foreach (PlayerLocation playerLoc in playerLocations)
            {
                PlayerLocation playerLocationToAdd = new PlayerLocation(playerLoc.X, playerLoc.Y, playerLoc.Z, playerLoc.LocationNumber);

                this.PlayerLocations.Add(playerLocationToAdd);
                Console.WriteLine(playerLoc.X + " " + playerLoc.Y + " " + playerLoc.Z + " " + playerLoc.PlayerLocationId + " " + playerLoc.LocationNumber);
            }
        }

        public PlayerLevel()
        {

        }
    }
}
