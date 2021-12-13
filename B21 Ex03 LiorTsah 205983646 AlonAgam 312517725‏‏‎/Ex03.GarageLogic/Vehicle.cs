using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_PercentageOfEnergyLeft;
        protected List<Wheel> m_Wheels;
        protected Engine m_Engine;

        protected Vehicle(Engine i_Engine, List<Wheel> i_Wheels)
        {
            this.m_Engine = i_Engine;
            this.m_PercentageOfEnergyLeft = CalculatePercent();
            this.m_Wheels = i_Wheels;
        }

        public float CalculatePercent()
        {
            float percent;
            if (m_Engine is Electric)
            {
                percent = (((Electric)m_Engine).RemainingBatteryTime / ((Electric)m_Engine).MaxBatteryTime) * 100;
            }
            else
            {
                percent = (((Fuel)m_Engine).CurrentAmountOfFuel / ((Fuel)m_Engine).MaxAmountOfFuel) * 100;
            }

            return percent;
        }

        public string ModelName
        {
            get => this.m_ModelName;

            set => this.m_ModelName = value;
        }

        public string LicenseNumber
        {
            get => this.m_LicenseNumber;

            set => this.m_LicenseNumber = value;
        }

        public float PercentageOfEnergyLeft
        {
            get => this.m_PercentageOfEnergyLeft;

            set => this.m_PercentageOfEnergyLeft = value;
        }

        public List<Wheel> Wheels
        {
            get => this.m_Wheels;

            set => this.m_Wheels = value;
        }

        public Engine Engine
        {
            get => this.m_Engine;

            set => this.m_Engine = value;
        }
    }
}
