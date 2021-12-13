namespace Ex03.GarageLogic
{
    public class DetailsOfVehicleInGarage
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eCurrentSituation m_CurrentSituation;
        private Vehicle m_Vehicle;

        public DetailsOfVehicleInGarage(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Vehicle = i_Vehicle;
            m_CurrentSituation = eCurrentSituation.InRepair;
        }

        public string OwnerName
        {
            get => m_OwnerName;

            set => m_OwnerName = value;
        }

        public string OwnerPhoneNumber
        {
            get => m_OwnerPhoneNumber;

            set => m_OwnerPhoneNumber = value;
        }

        public eCurrentSituation CurrentSituation
        {
            get => m_CurrentSituation;

            set => m_CurrentSituation = value;
        }

        public Vehicle VehicleInGarage
        {
            get => m_Vehicle;

            set => m_Vehicle = value;
        }
    }
}