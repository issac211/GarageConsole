using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsDangerousMaterials;
        private float m_CargoVolume;

        internal Truck(Engine i_Engine, string i_LicenseNumber, int i_NumberOfTires, float i_MaxEnergyAmount,
                            float i_MaxTirePressure, string i_TiresProducerName = "", string i_NameOfModel = "")
            : base(i_Engine, i_LicenseNumber, i_NumberOfTires, i_MaxEnergyAmount,
                            i_MaxTirePressure, i_TiresProducerName, i_NameOfModel)
        { }

        public bool IsDangerousMaterials
        {
            get { return m_IsDangerousMaterials; }
            set { m_IsDangerousMaterials = value; }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }

        public override void SetVehicleConditions(params string[] i_Args)
        {
            base.SetVehicleConditions(i_Args);
            IsDangerousMaterials = bool.Parse(i_Args[2]);
            CargoVolume = float.Parse(i_Args[3]);
        }

        public override List<string> GetConditionsParamsDescriptions()
        {
            List<string> allConditionsParams = base.GetConditionsParamsDescriptions();
            allConditionsParams.Add($"Is the cargo with dangerous materials ({true}/{false})");
            allConditionsParams.Add("Cargo Volume (float)");

            return allConditionsParams;
        }

        public override bool IsMatchedConditions(params string[] i_Args)
        {
            bool isBaseMatched = base.IsMatchedConditions(i_Args);
            bool dangerousMaterialsMatched = true;
            bool isCargoVolumeMatched = true;

            if (i_Args[2] != null)
            {
                dangerousMaterialsMatched = IsDangerousMaterials == bool.Parse(i_Args[2]);
            }

            if (i_Args[3] != null)
            {
                isCargoVolumeMatched = CargoVolume == float.Parse(i_Args[3]);
            }

            return isBaseMatched && dangerousMaterialsMatched && isCargoVolumeMatched;
        }

        public override string ToString()
        {
            StringBuilder truckDescription = new StringBuilder();

            truckDescription.AppendLine("Truck:");
            truckDescription.AppendLine(base.ToString());
            truckDescription.AppendLine();
            truckDescription.AppendLine($"Is the cargo with dangerous dangerous materials?: {IsDangerousMaterials}");
            truckDescription.AppendLine($"Cargo Volume: {CargoVolume}");

            return truckDescription.ToString();
        }
    }
}
