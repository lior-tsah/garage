using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base("The value must be between " + i_MinValue + " and " + i_MaxValue)
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Message)
            : base(i_Message)
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }

        public float MinValue
        {
            get => r_MinValue;
        }

        public float MaxValue
        {
            get => r_MaxValue;
        }
    }
}