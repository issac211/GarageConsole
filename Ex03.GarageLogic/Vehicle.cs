using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_NameOfModel;
        private readonly string r_LicenseNumber;
        private List<Tire> m_VehicleTires;
        private readonly Engine r_VehicleEngine;

        internal Vehicle(Engine i_Engine, string i_LicenseNumber, int i_NumberOfTires, float i_MaxEnergyAmount,
                            float i_MaxTirePressure, string i_TiresProducerName = "", string i_NameOfModel = "")
        {
            if (i_NumberOfTires < 0)
            {
                throw new ArgumentException(
                    string.Format("The Number Of Tires '{0}' is not possible, must be greater than or equal to 0"
                                        , i_NumberOfTires));
            }

            r_VehicleEngine = i_Engine;
            r_LicenseNumber = i_LicenseNumber;
            m_NameOfModel = i_NameOfModel;
            r_VehicleEngine.MaxEnergyAmount = i_MaxEnergyAmount;
            m_VehicleTires = new List<Tire>(i_NumberOfTires);
            for (int i = 0; i < i_NumberOfTires; i++)
            {
                m_VehicleTires.Add(new Tire(i_MaxTirePressure, i_TiresProducerName));
            }
        }

        public List<Tire> Tires
        {
            get { return m_VehicleTires; }
        }

        public Engine VehicleEngine
        {
            get { return r_VehicleEngine; }
        }

        public string NameOfModel
        {
            get { return m_NameOfModel; }
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public virtual void SetVehicleConditions(params string[] i_Args)
        {
            float currentEnergyAmount = float.Parse(i_Args[0]);
            float currentTiresPressure = float.Parse(i_Args[1]);

            r_VehicleEngine.CurrentEnergyAmount = currentEnergyAmount;
            foreach (Tire tire in m_VehicleTires)
            {
                tire.SetCurrentTirePressure(currentTiresPressure);
            }
        }

        public virtual List<string> GetConditionsParamsDescriptions()
        {
            List<string> allConditionsParamsDescriptions = r_VehicleEngine.GetConditionsParamsDescriptions();

            allConditionsParamsDescriptions.Add("Amount of air in the Tires (float)");

            return allConditionsParamsDescriptions;
        }

        protected int GetParsedIntInRange(string i_Number, int i_FirstValue, int i_LastValue)
        {
            int number = int.Parse(i_Number);

            if (number > i_LastValue || number < i_FirstValue)
            {
                throw new ValueOutOfRangeException(number, i_FirstValue, i_LastValue);
            }

            return number;
        }

        public virtual bool IsMatchedConditions(params string[] i_Args)
        {
            bool isEnergyAmountMatched;

            if (i_Args[0] != null)
            {
                isEnergyAmountMatched = r_VehicleEngine.CurrentEnergyAmount == float.Parse(i_Args[0]);
            }
            else
            {
                isEnergyAmountMatched = true;
            }
            
            bool isTirePressureMatched = true;

            if(i_Args[1] != null)
            {
                float tiresPressure = float.Parse(i_Args[1]);

                foreach (Tire tire in m_VehicleTires)
                {
                    if (!tire.IsMatchedTirePressure(tiresPressure))
                    {
                        isTirePressureMatched = false;
                        break;
                    }
                }
            }
            
            return isEnergyAmountMatched && isTirePressureMatched;
        }

        public override string ToString()
        {
            int tireIndex = 1;
            StringBuilder vehicleDescription = new StringBuilder();

            vehicleDescription.AppendLine($"License Number: {LicenseNumber}");
            vehicleDescription.AppendLine($"Name Of Model: {NameOfModel}");
            vehicleDescription.AppendLine($"Engine:");
            vehicleDescription.AppendLine(VehicleEngine.ToString());
            vehicleDescription.AppendLine();
            vehicleDescription.AppendLine("Tires:");
            foreach (Tire tire in Tires)
            {
                vehicleDescription.AppendLine();
                vehicleDescription.AppendLine($"Tire number {tireIndex}:");
                vehicleDescription.AppendLine(tire.ToString());
                tireIndex++;
            }

            return vehicleDescription.ToString();
        }
    }
}