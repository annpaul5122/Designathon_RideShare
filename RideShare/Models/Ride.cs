using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Models
{
    public class Ride
    {
        public int RideId { get; set; }
        public int DriverId { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int AvailableSeats { get; set; }
    }

}
