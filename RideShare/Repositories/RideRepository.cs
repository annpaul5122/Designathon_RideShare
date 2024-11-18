using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RideShare.Models;
using RideShare.Utility;

public class RideRepository : IRideRepository
{
    public int AddDriver(string name)
    {
        using (var connection = DbUtility.GetConnection())
        {
            connection.Open();
            string query = "INSERT INTO Drivers (Name) OUTPUT INSERTED.DriverId VALUES (@Name);";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                return (int)command.ExecuteScalar();
            }
        }
    }

    public void AddRide(Ride ride)
    {
        using (var connection = DbUtility.GetConnection())
        {
            connection.Open();
            string query = "INSERT INTO Rides (DriverId, Destination, Date, Time, AvailableSeats) VALUES (@DriverId, @Destination, @Date, @Time, @AvailableSeats);";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DriverId", ride.DriverId);
                command.Parameters.AddWithValue("@Destination", ride.Destination);
                command.Parameters.AddWithValue("@Date", ride.Date);
                command.Parameters.AddWithValue("@Time", ride.Time);
                command.Parameters.AddWithValue("@AvailableSeats", ride.AvailableSeats);
                command.ExecuteNonQuery();
            }
        }
    }

    public IEnumerable<Ride> SearchRides(string destination)
    {
        var rides = new List<Ride>();
        using (var connection = DbUtility.GetConnection())
        {
            connection.Open();
            string query = "SELECT * FROM Rides WHERE Destination = @Destination AND AvailableSeats > 0;";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Destination", destination);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rides.Add(new Ride
                        {
                            RideId = (int)reader["RideId"],
                            DriverId = (int)reader["DriverId"],
                            Destination = reader["Destination"].ToString(),
                            Date = (DateTime)reader["Date"],
                            Time = (TimeSpan)reader["Time"],
                            AvailableSeats = (int)reader["AvailableSeats"]
                        });
                    }
                }
            }
        }
        return rides;
    }

    public void BookRide(int riderId, int rideId)
    {
        using (var connection = DbUtility.GetConnection())
        {
            connection.Open();

            // Add booking
            string bookingQuery = "INSERT INTO Bookings (RiderId, RideId) VALUES (@RiderId, @RideId);";
            using (var command = new SqlCommand(bookingQuery, connection))
            {
                command.Parameters.AddWithValue("@RiderId", riderId);
                command.Parameters.AddWithValue("@RideId", rideId);
                command.ExecuteNonQuery();
            }

            // Update seats
            string updateSeatsQuery = "UPDATE Rides SET AvailableSeats = AvailableSeats - 1 WHERE RideId = @RideId;";
            using (var command = new SqlCommand(updateSeatsQuery, connection))
            {
                command.Parameters.AddWithValue("@RideId", rideId);
                command.ExecuteNonQuery();
            }
        }
    }

    public int AddRider(string name)
    {
        using (var connection = DbUtility.GetConnection())
        {
            connection.Open();
            string query = "INSERT INTO Riders (Name) OUTPUT INSERTED.RiderId VALUES (@Name);";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                return (int)command.ExecuteScalar();
            }
        }
    }
}
