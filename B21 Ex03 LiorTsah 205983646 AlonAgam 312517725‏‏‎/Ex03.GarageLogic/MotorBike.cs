using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class MotorBike : Vehicle
    {
        private int m_EngineCapacity;
        private eLicenseTypes m_licenseType;

        public MotorBike(
            Engine i_Engine,
            List<Wheel> i_Wheels)
            : base(i_Engine, i_Wheels)
        {
        }

        public int EngineCapacity
        {
            get => m_EngineCapacity;

            set => m_EngineCapacity = value;
        }

        public eLicenseTypes LicenseType
        {
            get => m_licenseType;

            set => m_licenseType = value;
        }
    }

    public enum eLicenseTypes
    {
        A,
        B1,
        AA,
        BB
    }
}