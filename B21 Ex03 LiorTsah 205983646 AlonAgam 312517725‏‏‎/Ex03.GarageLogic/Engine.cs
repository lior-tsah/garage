namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public virtual void BatteryCharging(float i_NumOfHoursToCharge)
        {
        }

        public virtual void Refuel(float i_FuelToAdd, eTypeOfFuels i_TypeOfFuel)
        {
        }
    }

    public enum eTypeOfFuels
    {
        Soler,
        Octan98,
        Octan96,
        Octan95
    }
}