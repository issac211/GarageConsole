using System.Text;

namespace Ex03.GarageLogic
{
    public class StoredVehicle
    {
        private string m_OwnerName;
        private string m_OwnerPhone;
        private eVehicleStatus m_VehicleStatus;
        private readonly Vehicle r_Vehicle;

        internal StoredVehicle(Vehicle i_Vehicle)
        {
            r_Vehicle = i_Vehicle;
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
        }

        public string OwnerPhone
        {
            get { return m_OwnerPhone; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
        }

        public Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }

        internal void SetData(string i_OwnerName, string i_OwnerPhone, eVehicleStatus i_VehicleStatus)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_VehicleStatus = i_VehicleStatus;
        }

        internal void SetVehicleStatus(eVehicleStatus i_VehicleStatus)
        {
            m_VehicleStatus = i_VehicleStatus;
        }

        public override string ToString()
        {
            StringBuilder storedVehicleDescription = new StringBuilder();

            storedVehicleDescription.AppendLine("Stored Vehicle Description:");
            storedVehicleDescription.AppendLine($"Owner Name: {OwnerName}");
            storedVehicleDescription.AppendLine($"Owner Phone: {OwnerPhone}");
            storedVehicleDescription.AppendLine($"Vehicle Status: {VehicleStatus}");
            storedVehicleDescription.AppendLine();
            storedVehicleDescription.AppendLine($"The Vehicle Description:");
            storedVehicleDescription.AppendLine(Vehicle.ToString());
            
            return storedVehicleDescription.ToString();
        }
    }
}
