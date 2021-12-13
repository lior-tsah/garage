using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eDoors m_NumberOfDoors;
        private eColors m_Color;

        public Car(Engine i_Engine, List<Wheel> i_Wheels) : base(i_Engine, i_Wheels)
        {
        }

        public eColors Color
        {
            get => this.m_Color;

            set => this.m_Color = value;
        }

        public eDoors NumberOfDoors
        {
            get => this.m_NumberOfDoors;

            set => this.m_NumberOfDoors = value;
        }
    }

    public enum eColors
    {
        Red,
        White,
        Silver,
        Black
    }

    public enum eDoors
    {
        Two,
        Three,
        Four,
        Five
    }
}