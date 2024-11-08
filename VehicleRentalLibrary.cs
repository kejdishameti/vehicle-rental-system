using System;
namespace VehicleRentalLibrary
{
    public interface IRentable
    {
        bool Rent();
        bool Return();
    }

    public abstract class Vehicle : IRentable
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

        public virtual bool Rent()
        {
            if (IsRented)
            {
                Console.WriteLine("Vehicle is already rented");
                return false;
            }
            IsRented = true;
            Console.WriteLine($"Vehicle {Model} has been rented successfully.");
            return true;
        }

        public virtual bool Return()
        {
            if (!IsRented)
            {
                Console.WriteLine("Vehicle is already available");
                return false;
            }
            IsRented = false;
            Console.WriteLine("$Vehicle {Model} has been returned successfully.");
            return true;
        }
    }

    public class Car : Vehicle
    {
        public int NumberOfDoors { get; set; }

        public Car(string model, string licensePlate, int numberOfDoors)
            : base(model, licensePlate)
        {
            NumberOfDoors = numberOfDoors;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Number of Doors: {NumberOfDoors}");
        }
    }

    public class Bike : Vehicle
    {
        public string Type { get; set; }
        public Bike(string model, string licensePlate, string type)
            : base(model, licensePlate)
        {
            Type = type;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Type: {Type}");
        }
    }
}