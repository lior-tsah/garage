using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private VehiclesCreator m_VehiclesCreator;
        private List<DetailsOfVehicleInGarage> m_Details;
		
        public VehiclesCreator VehiclesCreator
        {
            get => m_VehiclesCreator;

            set => m_VehiclesCreator = value;
        }

        public List<DetailsOfVehicleInGarage> Details
        {
            get => m_Details;

            set => m_Details = value;
        }

        public Garage()
        {
            Details = new List<DetailsOfVehicleInGarage>();
            VehiclesCreator = new VehiclesCreator();
        }

        public void InsertVehicle(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            DetailsOfVehicleInGarage details = new DetailsOfVehicleInGarage(i_OwnerName, i_OwnerPhoneNumber, i_Vehicle);
            this.Details.Add(details);
        }

        public void ChangeCurrentSituationOfVehicle(eCurrentSituation i_CurrentSituation, DetailsOfVehicleInGarage i_Vehicle)
        {
            i_Vehicle.CurrentSituation = i_CurrentSituation;
        }

        // $G$ CSS-013 (-3) Bad variable name (should be in the form of: i_CamelCase).
        public void InflateWheelsAirPressureToMaximum(DetailsOfVehicleInGarage i_vehicle)
        {
            foreach (Wheel wheel in i_vehicle.VehicleInGarage.Wheels)
            {
                wheel.Inflate(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public void Refuel(eTypeOfFuels i_TypeOfFuel, float i_FuelToAdd, DetailsOfVehicleInGarage i_Vehicle)
        {
            i_Vehicle.VehicleInGarage.Engine.Refuel(i_FuelToAdd, i_TypeOfFuel);
            i_Vehicle.VehicleInGarage.PercentageOfEnergyLeft = i_Vehicle.VehicleInGarage.CalculatePercent();
        }

        public void Charge(float i_MinutesToCharge, DetailsOfVehicleInGarage i_Vehicle)
        {
            i_Vehicle.VehicleInGarage.Engine.BatteryCharging(i_MinutesToCharge);
            i_Vehicle.VehicleInGarage.PercentageOfEnergyLeft = i_Vehicle.VehicleInGarage.CalculatePercent();
        }
    }

    public enum eCurrentSituation
    {
        InRepair,
        Fixed,
        PaidUp
    }
}