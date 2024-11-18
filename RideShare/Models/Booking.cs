using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int RiderId { get; set; }
        public int RideId { get; set; }
    }

}
