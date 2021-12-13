namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_NameOfManufacturer;


        private float m_MaxAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_NameOfManufacturer, float i_MaxAirPressure)
        {
            this.m_NameOfManufacturer = i_NameOfManufacturer;
            this.m_MaxAirPressure = i_MaxAirPressure;
            this.m_CurrentAirPressure = this.MaxAirPressure / 2;
        }

        public float MaxAirPressure
        {
            get => m_MaxAirPressure;

            set => m_MaxAirPressure = value;
        }

        public string NameOfManufacturer
        {
            get => m_NameOfManufacturer;

            set => m_NameOfManufacturer = value;
        }

        public float CurrentAirPressure
        {
            get => m_CurrentAirPressure;

            set => m_CurrentAirPressure = value;
        }

        public void Inflate(float i_AirToAdd)
        {
            if (this.CurrentAirPressure + i_AirToAdd > MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure, "The maximum air pressure can be " + m_MaxAirPressure);
            }

            this.CurrentAirPressure += i_AirToAdd;
        }
    }
}
