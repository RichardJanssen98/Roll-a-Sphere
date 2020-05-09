using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerDataService.Models
{
    public class PlayerLevel
    {
        public int PlayerLevelId { get; set; }
        public int Level { get; set; }
        public string EmailPlayer { get; set; }
        public string Username { get; set; }

        public virtual ICollection<PlayerLocation> PlayerLocations { get; set; }
    }
}
