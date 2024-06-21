using System;
using System.Collections.Generic;
using static Ex03.GrarageLogic.Fuel;
using static Ex03.GrarageLogic.Car;
using static Ex03.GrarageLogic.Motorcycle;
using static Ex03.GrarageLogic.Truck;

namespace Ex03.GrarageLogic
{
    public class VehicleMaker
    {
        public const float k_NumVehicleOptions = 5f;

        public static readonly List<string> sr_VehicleTypes = new List<string> { "Fuel Car", "Electric Car", "Fuel Motorcycle", "Electric Motorcycle", "Truck" };

        public enum eVehicleOptions
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck,
        }

        public static Vehicle MakeVehicle(string i_LicenseNumber, eVehicleOptions i_VehicleOption)
        {
            Vehicle vehicle;
            EnergyResource engine;

            switch (i_VehicleOption)
            {
                case eVehicleOptions.FuelCar:
                    vehicle = new Car(i_LicenseNumber);
                    engine = new Fuel(Car.k_MaxFuelAmount, Car.k_FuelType);
                    break;
                case eVehicleOptions.ElectricCar:
                    vehicle = new Car(i_LicenseNumber);
                    engine = new Electric(Car.k_MaxBatteryTimeInHours);
                    break;
                case eVehicleOptions.FuelMotorcycle:
                    vehicle = new Motorcycle(i_LicenseNumber);
                    engine = new Fuel(Motorcycle.k_MaxFuelAmount, Motorcycle.k_FuelType);
                    break;
                case eVehicleOptions.ElectricMotorcycle:
                    vehicle = new Motorcycle(i_LicenseNumber);
                    engine = new Electric(Motorcycle.k_MaxBatteryTimeInHours);
                    break;
                case eVehicleOptions.Truck:
                    vehicle = new Truck(i_LicenseNumber);
                    engine = new Fuel(Truck.k_MaxFuelAmount, Truck.k_FuelType);
                    break;
                default:
                    throw new ValueOutOfRangeException((float)eVehicleOptions.FuelCar,
                        k_NumVehicleOptions);

            }

            vehicle.Engine = engine;
            return vehicle;
        }
    }
}