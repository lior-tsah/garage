using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsDrivingDangerousMaterials;
        private float m_MaximumCarryingWeight;

        public Truck(Engine i_Engine, List<Wheel> i_Wheels)
            : base(i_Engine, i_Wheels)
        {
        }

        public bool IsDrivingDangerousMaterials
        {
            get => m_IsDrivingDangerousMaterials;

            set => m_IsDrivingDangerousMaterials = value;
        }

        public float MaximumCarryingWeight
        {
            get => this.m_MaximumCarryingWeight;

            set => this.m_MaximumCarryingWeight = value;
        }
    }
}