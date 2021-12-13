using System;

namespace Ex03.GarageLogic
{
    public class Fuel : Engine
    {

        // $G$ DSN-999 (-4) The "fuel type" field should be readonly member of class FuelEnergyProvider.
        protected eTypeOfFuels m_TypeOfFuel;
        protected float m_CurrentAmountOfFuel;
        protected float m_MaxAmountOfFuel;

        public Fuel(eTypeOfFuels i_TypeOfFuel, float i_MaxAmountOfFuel)
        {
            this.m_TypeOfFuel = i_TypeOfFuel;
            this.m_MaxAmountOfFuel = i_MaxAmountOfFuel;
            this.m_CurrentAmountOfFuel = this.m_MaxAmountOfFuel / 2;
        }

        public override void Refuel(float i_FuelToAdd, eTypeOfFuels i_TypeOfFuel)
        {
            if (i_FuelToAdd + m_CurrentAmountOfFuel > m_MaxAmountOfFuel)
            {
                throw new ValueOutOfRangeException(0, m_MaxAmountOfFuel, "The max fuel capacity can be " + m_MaxAmountOfFuel);
            }

            if (this.m_TypeOfFuel != i_TypeOfFuel)
            {
                throw new ArgumentException("This type of the fuel that you entered is not suitable for this vehicle");
            }

            m_CurrentAmountOfFuel += i_FuelToAdd;
        }

        public float CurrentAmountOfFuel
        {
            get => this.m_CurrentAmountOfFuel;

            set => this.m_CurrentAmountOfFuel = value;
        }

        public float MaxAmountOfFuel
        {
            get => this.m_MaxAmountOfFuel;

            set => this.m_MaxAmountOfFuel = value;
        }

        public eTypeOfFuels eType
        {
            get => this.m_TypeOfFuel;

            set => this.m_TypeOfFuel = value;
        }
    }
}
