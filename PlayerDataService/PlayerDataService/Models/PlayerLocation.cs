using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerDataService.Models
{
    public class PlayerLocation
    {
        public int PlayerLocationId { get; set; }
        public int LocationNumber { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
