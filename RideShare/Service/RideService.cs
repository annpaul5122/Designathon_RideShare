using RideShare.Models;
using RideShare.Service;


public class RideService : IRideService
{
    private readonly IRideRepository _rideRepository;

    public RideService(IRideRepository rideRepository)
    {
        _rideRepository = rideRepository;
    }

    public void AddRide(string driverName, Ride ride)
    {
        int driverId = _rideRepository.AddDriver(driverName);
        ride.DriverId = driverId;
        _rideRepository.AddRide(ride);
    }

    public IEnumerable<Ride> SearchRides(string destination)
    {
        return _rideRepository.SearchRides(destination);
    }

    public void BookRide(string riderName, int rideId)
    {
        int riderId = _rideRepository.AddRider(riderName);
        _rideRepository.BookRide(riderId, rideId);
    }
}
