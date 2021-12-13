using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehiclesCreator
    {
        private readonly List<Vehicle> r_Vehicles;

        public VehiclesCreator()
        {
            r_Vehicles = new List<Vehicle>();
            this.createListOfSupportedVehicles();
        }

        public List<Wheel> CreateListOfWheel(int i_NumberOfWheels, float i_MaxAirPressure)
        {
            List<Wheel> lstWheels = new List<Wheel>();
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel(" Michelin", i_MaxAirPressure);
                lstWheels.Add(wheel);
            }

            return lstWheels;
        }

        private Truck createDefaultTruck()
        {
            return new Truck(new Fuel(eTypeOfFuels.Soler, 120), CreateListOfWheel(16, 26));
        }

        private MotorBike createDefaultRegularMotorBike()
        {
            return new MotorBike(new Fuel(eTypeOfFuels.Octan98, 6), CreateListOfWheel(2, 30));
        }

        private MotorBike createDefaultElectricMotorBike()
        {
            return new MotorBike(new Electric(1.8f), CreateListOfWheel(2, 30));
        }

        private Car createDefaultElectricCar()
        {
            return new Car(new Electric(3.2f), CreateListOfWheel(4, 32));
        }

        private Car createDefaultRegularCar()
        {
            return new Car(new Fuel(eTypeOfFuels.Octan95, 45), CreateListOfWheel(4, 32));
        }

        private void createListOfSupportedVehicles()
        {
            this.r_Vehicles.Add(createDefaultTruck());
            this.r_Vehicles.Add(createDefaultRegularMotorBike());
            this.r_Vehicles.Add(createDefaultElectricMotorBike());
            this.r_Vehicles.Add(createDefaultRegularCar());
            this.r_Vehicles.Add(createDefaultElectricCar());
        }

        public Truck CreateTruck(string i_ModelName, string i_LicenseNumber, bool i_IsDrivingDangerousMaterials, float i_MaximumCarryingWeight)
        {
            Truck truck = createDefaultTruck();
            truck.ModelName = i_ModelName;
            truck.LicenseNumber = i_LicenseNumber;
            truck.IsDrivingDangerousMaterials = i_IsDrivingDangerousMaterials;
            truck.MaximumCarryingWeight = i_MaximumCarryingWeight;
            return truck;
        }

        public MotorBike CreateMotorBike(string i_ModelName, string i_LicenseNumber, int i_EngineCapacity, eLicenseTypes i_LicenseType, bool i_IsElectric)
        {
            MotorBike motorBike = i_IsElectric ? createDefaultElectricMotorBike() : createDefaultRegularMotorBike();

            motorBike.ModelName = i_ModelName;
            motorBike.LicenseNumber = i_LicenseNumber;
            motorBike.EngineCapacity = i_EngineCapacity;
            motorBike.LicenseType = i_LicenseType;
            return motorBike;
        }

        public Car CreateCar(string i_ModelName, string i_LicenseNumber, eDoors i_NumberOfDoors, eColors i_Color, bool i_IsElectric)
        {
            Car car = i_IsElectric ? createDefaultElectricCar() : createDefaultRegularCar();

            car.ModelName = i_ModelName;
            car.LicenseNumber = i_LicenseNumber;
            car.NumberOfDoors = i_NumberOfDoors;
            car.Color = i_Color;
            return car;
        }
    }
}
