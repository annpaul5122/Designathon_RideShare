using System;
using RideShare.Models;
using RideShare.Service;


class Program
{
    static void Main()
    {
        IRideRepository rideRepository = new RideRepository();
        IRideService rideService = new RideService(rideRepository);

        Console.WriteLine("Welcome to Hexaware RideShare!");

        while (true)
        {
            Console.WriteLine("\n1. Add Ride (Driver)");
            Console.WriteLine("2. Search Rides (Rider)");
            Console.WriteLine("3. Book Ride (Rider)");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine() ?? "4");

            switch (choice)
            {
                case 1:
                    Console.Write("Driver Name: ");
                    string driverName = Console.ReadLine();
                    Console.Write("Destination: ");
                    string destination = Console.ReadLine();
                    Console.Write("Date (YYYY-MM-DD): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    Console.Write("Time (HH:MM): ");
                    TimeSpan time = TimeSpan.Parse(Console.ReadLine());
                    Console.Write("Available Seats: ");
                    int seats = int.Parse(Console.ReadLine());

                    Ride ride = new Ride { Destination = destination, Date = date, Time = time, AvailableSeats = seats };
                    rideService.AddRide(driverName, ride);
                    Console.WriteLine("Ride added successfully!");
                    break;

                case 2:
                    Console.Write("Destination: ");
                    string searchDestination = Console.ReadLine();
                    var rides = rideService.SearchRides(searchDestination);
                    Console.WriteLine("\nAvailable Rides:");
                    foreach (var r in rides)
                    {
                        Console.WriteLine($"Ride ID: {r.RideId}, Destination: {r.Destination}, Seats: {r.AvailableSeats}");
                    }
                    break;

                case 3:
                    Console.Write("Rider Name: ");
                    string riderName = Console.ReadLine();
                    Console.Write("Ride ID: ");
                    int rideId = int.Parse(Console.ReadLine());
                    rideService.BookRide(riderName, rideId);
                    Console.WriteLine("Ride booked successfully!");
                    break;

                case 4:
                    Console.WriteLine("Exiting... Thank you for using RideShare!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
