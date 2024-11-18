using RideShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Service
{
    public interface IRideService
    {
        void AddRide(string driverName, Ride ride);
        IEnumerable<Ride> SearchRides(string destination);
        void BookRide(string riderName, int rideId);
    }
}
