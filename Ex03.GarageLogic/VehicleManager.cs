using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class VehicleManager
    {
        private readonly Dictionary<string, StoredVehicle> r_StoredVehiclesInGarage;
        private readonly List<string> r_SharedConditionsDescriptions;

        public VehicleManager()
        {
            r_StoredVehiclesInGarage = new Dictionary<string, StoredVehicle>();
            r_SharedConditionsDescriptions = new List<string>();
        }

        public StoredVehicle StoreTheVehicleInGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            StoredVehicle storedVehicle = new StoredVehicle(i_Vehicle);
            List<string> vehicleConditionsDescriptions = i_Vehicle.GetConditionsParamsDescriptions();

            storedVehicle.SetData(i_OwnerName, i_OwnerPhone, eVehicleStatus.InProgress);
            r_StoredVehiclesInGarage[i_Vehicle.LicenseNumber] = storedVehicle;
            if (r_SharedConditionsDescriptions.Count == 0)
            {
                foreach (string conditions in vehicleConditionsDescriptions)
                {
                    r_SharedConditionsDescriptions.Add(conditions);
                }
            }
            else
            {
                int minSharedIndex = r_SharedConditionsDescriptions.Count < vehicleConditionsDescriptions.Count ?
                                        r_SharedConditionsDescriptions.Count : vehicleConditionsDescriptions.Count;

                if (minSharedIndex > 0)
                {
                    for (int i = 0; i < minSharedIndex; i++)
                    {
                        if (r_SharedConditionsDescriptions[i] != vehicleConditionsDescriptions[i])
                        {
                            r_SharedConditionsDescriptions[i] = null;
                        }
                    }
                }
            }

            return storedVehicle;
        }

        public bool TryGetStoredVehicle(string i_LicenseNumber, out StoredVehicle o_StoredVehicle)
        {
            bool isStoredVehicleInGarage;

            isStoredVehicleInGarage = r_StoredVehiclesInGarage.TryGetValue(i_LicenseNumber, out o_StoredVehicle);

            return isStoredVehicleInGarage;
        }

        public bool ChangeVehicleStatus(string i_LicenseNumber, string i_newVehicleStatus)
        {
            bool isStoredVehicleInGarage = TryGetStoredVehicle(i_LicenseNumber, out StoredVehicle storedVehicle);
            bool newVehicleSIsOk = false;

            if (isStoredVehicleInGarage)
            {
                newVehicleSIsOk = Enum.TryParse(i_newVehicleStatus, out eVehicleStatus vehicleStatusInput);

                if (newVehicleSIsOk)
                {
                    storedVehicle.SetVehicleStatus(vehicleStatusInput);
                }
            }

            return newVehicleSIsOk;
        }

        public string[] StoredVehiclesInGarage
        {
            get
            {
                return r_StoredVehiclesInGarage.Keys.ToArray();
            }
        }

        public bool InflateVehicleTiresToMax(string i_LicenseNumber)
        {
            bool isStoredVehicleInGarage = TryGetStoredVehicle(i_LicenseNumber, out StoredVehicle storedVehicle);

            if (isStoredVehicleInGarage && storedVehicle.Vehicle.Tires != null)
            {
                foreach(Tire tire in storedVehicle.Vehicle.Tires)
                {
                    tire.SetCurrentTirePressure(tire.MaxTirePressure);
                }
            }

            return isStoredVehicleInGarage && storedVehicle.Vehicle.Tires != null;
        }

        public string[] GetSharedConditionsDescriptions()
        {
            List<string> sharedConditionsDescriptions = new List<string>();

            foreach(string sharedCondition in r_SharedConditionsDescriptions)
            {
                if(sharedCondition != null)
                {
                    sharedConditionsDescriptions.Add(sharedCondition);
                }
            }

            return sharedConditionsDescriptions.ToArray();
        }

        public string[] GetFilteredVehiclesInGarage(params string[] i_Args)
        {
            List<string> filteredVehicles = new List<string>();
            List<string> newArgs = new List<string>();
            int argsIndex = 0;

            foreach (string sharedCondition in r_SharedConditionsDescriptions)
            {
                if(sharedCondition == null)
                {
                    newArgs.Add(sharedCondition);
                }
                else
                {
                    newArgs.Add(i_Args[argsIndex]);
                    argsIndex++;
                }
            }

            foreach (StoredVehicle storedVehicle in r_StoredVehiclesInGarage.Values)
            {
                Vehicle vehicle = storedVehicle.Vehicle;

                if (vehicle.IsMatchedConditions(newArgs.ToArray()))
                {
                    filteredVehicles.Add(vehicle.LicenseNumber);
                }
            }
            
            return filteredVehicles.ToArray();
        }
    }
}
