namespace Ex03.GarageLogic
{
    public class Electric : Engine
    {
        protected float m_RemainingBatteryTime;
        protected float m_MaxBatteryTime;

        public Electric(float i_MaxBatteryTime)
        {
            this.MaxBatteryTime = i_MaxBatteryTime;
            this.RemainingBatteryTime = i_MaxBatteryTime / 2;
        }

        public override void BatteryCharging(float i_NumOfHoursToCharge)
        {
            if (i_NumOfHoursToCharge + m_RemainingBatteryTime > MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(0, m_MaxBatteryTime, "The max battery capacity can be " + m_MaxBatteryTime);
            }

            this.m_RemainingBatteryTime += i_NumOfHoursToCharge;
        }

        public float RemainingBatteryTime
        {
            get => this.m_RemainingBatteryTime;

            set => this.m_RemainingBatteryTime = value;
        }

        public float MaxBatteryTime
        {
            get => this.m_MaxBatteryTime;

            set => this.m_MaxBatteryTime = value;
        }
    }
}