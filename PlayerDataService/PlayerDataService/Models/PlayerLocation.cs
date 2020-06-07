using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerDataService.Models
{
    [Serializable]
    public class PlayerLocation
    {
        public int PlayerLocationId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public int LocationNumber { get; set; }

        public PlayerLocation (float x, float y, float z, int locationNumber)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.LocationNumber = locationNumber;
        }

        public PlayerLocation()
        {

        }
    }
}
