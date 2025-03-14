using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private const int k_NumOfTiresOnCar = 5;
        private const int k_MaxTirePressureOnCar = 30;
        private const FuelEngine.eFuelType k_FuelTypeForCar = FuelEngine.eFuelType.Octan95;
        private const float k_MaxEnergyAmountForFuelCar = 58f;
        private const float k_MaxEnergyAmountForElectricCar = 4.8f;
        private const int k_NumOfTiresOnMotorcycle = 2;
        private const int k_MaxTirePressureOnMotorcycle = 29;
        private const FuelEngine.eFuelType k_FuelTypeForMotorcycle = FuelEngine.eFuelType.Octan98;
        private const float k_MaxEnergyAmountForFuelMotorcycle = 5.8f;
        private const float k_MaxEnergyAmountForElectricMotorcycle = 2.8f;
        private const int k_NumOfTiresOnFuelTruck = 12;
        private const int k_MaxTirePressureOnFuelTruck = 28;
        private const FuelEngine.eFuelType k_FuelTypeForFuelTruck = FuelEngine.eFuelType.Soler;
        private const float k_MaxEnergyAmountForFuelTruck = 110f;
        private readonly List<string> r_SupportedVehicles;
        private readonly eVehicleType[] r_VehicleTypes;

        public VehicleFactory()
        {
            string[] supportedVehicles = new string[]
            {
                $"({k_NumOfTiresOnCar} Tires, {k_MaxTirePressureOnCar} maximum air pressure, {k_FuelTypeForCar} fuel type, {k_MaxEnergyAmountForFuelCar} liter fuel tank)",
                $"({k_NumOfTiresOnCar} Tiers, {k_MaxTirePressureOnCar} maximum air pressure, {k_MaxEnergyAmountForElectricCar} Maximum baterry time)",
                $"({k_NumOfTiresOnMotorcycle} Tires, {k_MaxTirePressureOnMotorcycle} maximum air pressure, {k_FuelTypeForMotorcycle} fuel type, {k_MaxEnergyAmountForFuelMotorcycle} liter fuel tank)",
                $"({k_NumOfTiresOnMotorcycle} Tires, {k_MaxTirePressureOnMotorcycle} maximum air pressure, {k_MaxEnergyAmountForElectricMotorcycle} Maximum baterry time)",
                $"({k_NumOfTiresOnFuelTruck} Tires, {k_MaxTirePressureOnFuelTruck} Maximum air pressure, {k_FuelTypeForFuelTruck} fuel type, {k_MaxEnergyAmountForFuelTruck} liter fuel tank)"
            };

            r_VehicleTypes = (eVehicleType[])Enum.GetValues(typeof(eVehicleType));
            r_SupportedVehicles = new List<string>();
            for (int i = 0; i < r_VehicleTypes.Length; i++)
            {
                r_SupportedVehicles.Add($"{(int)r_VehicleTypes[i]}. {r_VehicleTypes[i]} {supportedVehicles[i]}");
            }
        }

        public string[] SupportedVehicles
        {
            get { return r_SupportedVehicles.ToArray(); }
        }

        public Vehicle MakeVehicleWithVehicleTypeIndex(int i_VehicleTypeIndex, string i_LicenseNumber)
        {
            int firstValueVehicleType = (int)((eVehicleType[])Enum.GetValues(typeof(eVehicleType)))[0];
            int amountOfVehicleType = Enum.GetValues(typeof(eVehicleType)).Length;
            int lastValueVehicleType = amountOfVehicleType - 1 + firstValueVehicleType;
            bool isIndexInRange = i_VehicleTypeIndex >= firstValueVehicleType && i_VehicleTypeIndex <= lastValueVehicleType;

            if (!isIndexInRange)
            {
                throw new ValueOutOfRangeException(i_VehicleTypeIndex, firstValueVehicleType, lastValueVehicleType);
            }

            Vehicle vehicle = makeVehicle((eVehicleType)i_VehicleTypeIndex, i_LicenseNumber);

            return vehicle;
        }

        private Vehicle makeVehicle(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            Vehicle vehicle;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    vehicle = makeFuelCar(i_LicenseNumber);
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = makeElectricCar(i_LicenseNumber);
                    break;
                case eVehicleType.FuelMotorcycle:
                    vehicle = makeFuelMotorcycle(i_LicenseNumber);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = makeElectricMotorcycle(i_LicenseNumber);
                    break;
                case eVehicleType.FuelTruck:
                    vehicle = makeFuelTruck(i_LicenseNumber);
                    break;
                default:
                    vehicle = null;
                    break;
            }

            return vehicle;
        }

        private Car makeFuelCar(string i_LicenseNumber)
        {
            return new Car(new FuelEngine(k_FuelTypeForCar), i_LicenseNumber,
                        k_NumOfTiresOnCar, k_MaxEnergyAmountForFuelCar, k_MaxTirePressureOnCar,
                        "Producer1", "Model1");
        }

        private Car makeElectricCar(string i_LicenseNumber)
        {
            return new Car(new ElectricEngine(), i_LicenseNumber,
                        k_NumOfTiresOnCar, k_MaxEnergyAmountForElectricCar, k_MaxTirePressureOnCar,
                        "Producer2", "Model2");
        }

        private Motorcycle makeFuelMotorcycle(string i_LicenseNumber)
        {
            return new Motorcycle(new FuelEngine(k_FuelTypeForMotorcycle), i_LicenseNumber,
                        k_NumOfTiresOnMotorcycle, k_MaxEnergyAmountForFuelMotorcycle, k_MaxTirePressureOnMotorcycle,
                        "Producer3", "Model3");
        }

        private Motorcycle makeElectricMotorcycle(string i_LicenseNumber)
        {
            return new Motorcycle(new ElectricEngine(), i_LicenseNumber,
                        k_NumOfTiresOnMotorcycle, k_MaxEnergyAmountForElectricMotorcycle, k_MaxTirePressureOnMotorcycle,
                        "Producer4", "Model4");
        }

        private Truck makeFuelTruck(string i_LicenseNumber)
        {
            return new Truck(new FuelEngine(k_FuelTypeForFuelTruck), i_LicenseNumber,
                        k_NumOfTiresOnFuelTruck, k_MaxEnergyAmountForFuelTruck, k_MaxTirePressureOnFuelTruck,
                        "Producer5", "Model5");
        }
    }
}
