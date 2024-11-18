using System.Collections.Generic;
using RideShare.Models;

public interface IRideRepository
{
    int AddDriver(string name);
    void AddRide(Ride ride);
    IEnumerable<Ride> SearchRides(string destination);
    void BookRide(int riderId, int rideId);
    int AddRider(string name);
}
