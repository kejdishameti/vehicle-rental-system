using System.Xml.Schema;
using System.Collections.Generic;
using System.Globalization;

namespace VehicleRentalLibrary;

class Program
{
    static RentalService rentalService = new RentalService();

    static void Main(string[] args)
    {
        while (true)
        {
            DisplayMenu();
            string? choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice))
            {
                Console.WriteLine("Invalid input. Please try again.");
                return;
            }

            switch (choice)
            {
                case "1":
                    AddVehicle();
                    break;
                case "2":
                    RentVehicle();
                    break;
                case "3":
                    ReturnVehicle();
                    break;
                case "4":
                    rentalService.DisplayAllVehicles();
                    break;
                case "5":
                    rentalService.DisplayAvailableVehicles();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\nVehicle Rental System");
        Console.WriteLine("1. Add Vehicle");
        Console.WriteLine("2. Rent Vehicle");
        Console.WriteLine("3. Return Vehicle");
        Console.WriteLine("4. Display All Vehicles");
        Console.WriteLine("5. Display Available Vehicles");
        Console.WriteLine("6. Exit");
        Console.Write("Enter your choice: ");
    }

    static void AddVehicle()
    {
        Console.WriteLine("\nEnter vehicle type (1 for Car, 2 for Bike): ");
        string? type = Console.ReadLine();

        Console.WriteLine("Enter model: ");
        string? model = Console.ReadLine();

        Console.WriteLine("Enter license plate: ");
        string? licensePlate = Console.ReadLine();

        if (type == "1")
        {
            Console.WriteLine("Enter number of doors");
            if (int.TryParse(Console.ReadLine(), out int doors))
            {
                if (!string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(licensePlate))
                {
                    rentalService.AddVehicle(new Car(model, licensePlate, doors));
                    Console.WriteLine("Car added successfully!");
                }
                else
                {
                    Console.WriteLine("Model and license plate cannot be null. Please try again.");
                }

                rentalService.AddVehicle(new Car(model, licensePlate, doors));
                Console.WriteLine("Car added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid number of doors");
            }
        }

        else if (type == "2")
        {
            Console.Write("Enter Bike Type (Mountain/Road/City): ");
            string? bikeType = Console.ReadLine();
            rentalService.AddVehicle(new Bike(model, licensePlate, bikeType));
            Console.WriteLine("Bike added successfully!");
        }
        else
        {
            Console.WriteLine("Invalid vehicle type");
        }
    }

    static void RentVehicle()
    {
        Console.WriteLine("Enter licnese plate of vehicle that you want to rent: ");
        string? licensePlate = Console.ReadLine();

        if (!string.IsNullOrEmpty(licensePlate))
        {
            rentalService.RentVehicle(licensePlate);
        }
        else
        {
            Console.WriteLine("Invalid license plate. Please try again");
        }

        rentalService.RentVehicle(licensePlate);
    }

    static void ReturnVehicle()
    {
        Console.WriteLine("Enter license plate of the vehicle that you want to return: ");
        string? licensePlate = Console.ReadLine();

        if (!string.IsNullOrEmpty(licensePlate))
        {
            rentalService.RentVehicle(licensePlate);
        }
        else
        {
            Console.WriteLine("Invalid license plate. Please try again");
        }
        rentalService.ReturnVehicle(licensePlate);
    }
}

    public class RentalService
    {
        private List<Vehicle> vehicles = new List<Vehicle> ();

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        public void RentVehicle(string? licensePlate)
        {
            if(string.IsNullOrEmpty(licensePlate))
            {
                Console.WriteLine("Invalid license plate. Please try again");
                return;
            }

            Vehicle? vehicle = vehicles.Find(v => v.LicensePlate == licensePlate);
            if (vehicle != null)
            {
                vehicle.Rent();
            }
            else
            {
                Console.WriteLine("Vehicle not found.");
            }
        }

        public void ReturnVehicle(string? licensePlate)
        {
            if (string.IsNullOrEmpty(licensePlate))
            {
                Console.WriteLine("Invalid license plate. Please try again");
                return;
            }

            Vehicle? vehicle = vehicles.Find(v => v.LicensePlate == licensePlate);
            if (vehicle != null)
            {
                vehicle.Return();
            }
            else
            {
                Console.WriteLine("Vehicle not found.");
            }
        }

        public void DisplayAllVehicles()
        {
            if(vehicles.Count == 0)
            {
                Console.WriteLine("No vehicles in the system");
                return;
            }

            Console.WriteLine("\nAll Vehicles:");
            foreach(var vehicle in vehicles)
            {
                vehicle.DisplayDetails();
                Console.WriteLine("-----------");
            }
        }

        public void DisplayAvailableVehicles()
        {
            var availableVehicles = vehicles.Where(v => !v.IsRented).ToList();

            if(availableVehicles.Count == 0)
            {
                Console.WriteLine("No available vehicles");
                return;
            }

            Console.WriteLine("\nAvailable Vehicles:");
            foreach (var vehicle in availableVehicles)
            {
                vehicle.DisplayDetails();
                Console.WriteLine("----------");
            }
        }
    }

