using System.Xml.Schema;
using System.Collections.Generic;
using System.Globalization;

namespace Vehicle_Rental_System
{
    public abstract class Vehicle
    {
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public bool IsRented { get; set; }

        public Vehicle(string model, string licensePlate)
        {
            Model = model;
            LicensePlate = licensePlate;
            IsRented = false;
        }

        public virtual void DisplayDetails()
        {
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"License Plate: {LicensePlate}");
            Console.WriteLine($"Rent status: {(IsRented ? "Rented" : "Available")}");
        }
    }

    public class Car : Vehicle
    {
        public int Seats { get; set; }
        public Car(string model, string licensePlate, int seats)
            : base(model, licensePlate)
        {
            Seats = seats;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Seats: {Seats}");
        }
    }

    public class Bike : Vehicle
    {
        public bool HasBasket { get; set; }
        public Bike(string model, string licensePlate, bool hasBasket) :
            base(model, licensePlate)
        {
            HasBasket = hasBasket;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Basket:{(HasBasket ? "Yes" : "No")}");
        }
    }

    public class RentalService
    {
        private List<Vehicle> vehicles = new List<Vehicle>();
        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        public bool RentVehicle(string model, string licensePlate)
        {
            Vehicle vehicle = vehicles.Find(v => v.LicensePlate == licensePlate);

            if (vehicle == null)
            {
                Console.WriteLine("Vehicle not found");
                return false;
            }

            if (vehicle.IsRented)
            {
                Console.WriteLine("Vehicle is already rented");
                return false;
            }

            vehicle.IsRented = true;
            Console.WriteLine($"Vehicle {model} with {licensePlate} license plate has been rented successfully");
            return true;
        }

        public bool ReturnVehicle(string model, string licensePlate)
        {

            Vehicle vehicle = vehicles.Find(v => v.LicensePlate == licensePlate);

            if (vehicle == null)
            {
                Console.WriteLine("Vehicle not found.");
                return false;
            }

            if (!vehicle.IsRented)
            {
                Console.WriteLine("Vehicle is already available.");
                return false;
            }

            vehicle.IsRented = false;
            Console.WriteLine($"Vehicle {model} with {licensePlate} license plate has been returned successfully.");
            return true;
        }

        public void DisplayAllVehicles()
        {
            Console.WriteLine("All Vehicles: ");
            foreach (var vehicle in vehicles)
            {
                vehicle.DisplayDetails();
                Console.WriteLine("--------------");
            }
        }

        public void DisplayAvailableVehicles()
        {
            Console.WriteLine("Available Vehicles:");
            foreach (var vehicle in vehicles)
            {
                if (!vehicle.IsRented)
                {
                    vehicle.DisplayDetails();
                    Console.WriteLine("-------------------");
                }
            }
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            RentalService rentalService = new RentalService();

            rentalService.AddVehicle(new Car("Fiat Punto", "AB000EE", 6));
            rentalService.AddVehicle(new Car("Ford Focus", "AB111RR", 6));
            rentalService.AddVehicle(new Car("Ford Rangers", "AA134TE", 2));
            rentalService.AddVehicle(new Car("Toyota Land Cruiser", "AC434DE", 6));
            rentalService.AddVehicle(new Car("Opel Corsa", "AD232DD", 6));

            rentalService.DisplayAllVehicles();

            rentalService.RentVehicle("Ford Rangers", "AA134TE");

            rentalService.DisplayAvailableVehicles();

            rentalService.ReturnVehicle("Ford Rangers", "AA134TE");

            rentalService.DisplayAllVehicles();
        }
    }
}
