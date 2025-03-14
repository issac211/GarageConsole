using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private int m_EngineVolume;
        private eLicenseType m_LicenseType;

        public enum eLicenseType
        {
            A1 = 1,
            A2,
            AB,
            B2
        }

        internal Motorcycle(Engine i_Engine, string i_LicenseNumber, int i_NumberOfTires, float i_MaxEnergyAmount,
                            float i_MaxTirePressure, string i_TiresProducerName = "", string i_NameOfModel = "")
            : base(i_Engine, i_LicenseNumber, i_NumberOfTires, i_MaxEnergyAmount,
                            i_MaxTirePressure, i_TiresProducerName, i_NameOfModel)
        { }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
            set { m_EngineVolume = value; }
        }

        public override void SetVehicleConditions(params string[] i_Args)
        {
            base.SetVehicleConditions(i_Args);

            int firstValueLicenseType = (int)((eLicenseType[])Enum.GetValues(typeof(eLicenseType)))[0];
            int amountOfLicenseType = Enum.GetValues(typeof(eLicenseType)).Length;
            int lastValueLicenseType = amountOfLicenseType - 1 + firstValueLicenseType;

            LicenseType = (eLicenseType)GetParsedIntInRange(i_Args[2], firstValueLicenseType, lastValueLicenseType);
            EngineVolume = int.Parse(i_Args[3]);
        }

        public override List<string> GetConditionsParamsDescriptions()
        {
            List<string> allConditionsParams = base.GetConditionsParamsDescriptions();
            StringBuilder licenseTypeMessage = new StringBuilder();
            int index = (int)((eLicenseType[])Enum.GetValues(typeof(eLicenseType)))[0];

            licenseTypeMessage.AppendLine("License Type:");
            foreach (eLicenseType licenseType in Enum.GetValues(typeof(eLicenseType)))
            {
                licenseTypeMessage.AppendLine($"{index}) {licenseType}");
                index++;
            }

            allConditionsParams.Add(licenseTypeMessage.ToString());
            allConditionsParams.Add("Engine Volume (int)");

            return allConditionsParams;
        }

        public override bool IsMatchedConditions(params string[] i_Args)
        {
            bool isBaseMatched = base.IsMatchedConditions(i_Args);
            int firstValueLicenseType = (int)((eLicenseType[])Enum.GetValues(typeof(eLicenseType)))[0];
            int amountOfLicenseType = Enum.GetValues(typeof(eLicenseType)).Length;
            int lastValueLicenseType = amountOfLicenseType - 1 + firstValueLicenseType;
            bool isLicenseTypeMatched = true;
            bool isEngineVolumeMatched = true;

            if(i_Args[2] != null)
            {
                isLicenseTypeMatched =
                    LicenseType == (eLicenseType)GetParsedIntInRange(i_Args[2], firstValueLicenseType, lastValueLicenseType);
            }

            if (i_Args[3] != null)
            {
                isEngineVolumeMatched = EngineVolume == int.Parse(i_Args[3]);
            }

            return isLicenseTypeMatched && isEngineVolumeMatched && isBaseMatched;
        }

        public override string ToString()
        {
            StringBuilder motorcycleDescription = new StringBuilder();

            motorcycleDescription.AppendLine("Motorcycle:");
            motorcycleDescription.AppendLine(base.ToString());
            motorcycleDescription.AppendLine();
            motorcycleDescription.AppendLine($"License Type: {LicenseType}");
            motorcycleDescription.AppendLine($"Engine Volume: {EngineVolume}");

            return motorcycleDescription.ToString();
        }
    }
}
