using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set {  m_FuelType = value; }
        }

        public FuelEngine(eFuelType i_FuelType)
        {
            m_FuelType = i_FuelType;
        }

        public override void FillPower(params string[] i_Args)
        {
            eFuelType fuelType = fuelTypePars(i_Args[1]);

            if (fuelType != m_FuelType)
            {
                throw new ArgumentException(string.Format("The FuelType '{0}' does not match", fuelType));
            }

            base.FillPower(i_Args);
        }

        public override List<string> GetFillParamsDescriptions()
        {
            StringBuilder fuelTypeMessage = new StringBuilder();
            int firstValueFuelType = (int)((eFuelType[])Enum.GetValues(typeof(eFuelType)))[0];
            int index = firstValueFuelType;

            fuelTypeMessage.AppendLine("Fuel Type:");
            foreach (eFuelType fuelType in Enum.GetValues(typeof(eFuelType)))
            {
                fuelTypeMessage.AppendLine($"{index}) {fuelType}");
                index++;
            }

            return new List<string>() { "How much amount of fuel to fill (liters):", fuelTypeMessage.ToString() };
        }

        public override List<string> GetConditionsParamsDescriptions()
        {
            return new List<string>() { "Current amount of fuel (liters)" };
        }

        private eFuelType fuelTypePars(string i_FuelTypeNum)
        {
            int fuelTypeNum = int.Parse(i_FuelTypeNum);
            int firstValueFuelType = (int)((eFuelType[])Enum.GetValues(typeof(eFuelType)))[0];
            int amountOfFuelTypes = Enum.GetValues(typeof(eFuelType)).Length;
            int lastValueFuelType = amountOfFuelTypes - 1 + firstValueFuelType;

            if (fuelTypeNum > lastValueFuelType || fuelTypeNum < firstValueFuelType)
            {
                throw new ValueOutOfRangeException(fuelTypeNum, firstValueFuelType, lastValueFuelType);
            }

            return (eFuelType)fuelTypeNum;
        }

        public override string ToString()
        {
            StringBuilder fuelEngineDescription = new StringBuilder();

            fuelEngineDescription.AppendLine("fuel Engine");
            fuelEngineDescription.AppendLine(base.ToString());
            fuelEngineDescription.AppendLine($"Current amount of fuel (liters): {CurrentEnergyAmount}");
            fuelEngineDescription.AppendLine($"Max amount of fuel (liters): {MaxEnergyAmount}");
            fuelEngineDescription.AppendLine($"Fuel Type: {FuelType}");

            return fuelEngineDescription.ToString();
        }
    }
}
