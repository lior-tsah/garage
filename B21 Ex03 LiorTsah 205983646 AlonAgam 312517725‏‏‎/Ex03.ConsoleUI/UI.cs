using System;
using System.Text;
using System.Text.RegularExpressions;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        public enum eTypeOfVehicles
        {
            Truck,
            Car,
            MotorBike
        }

        public static void Main()
        {
            Garage garage = new Garage();
            Menu(garage);
        }

        public static void UserInput(Garage i_Garage)
        {
            while (true)
            {
                string choice = Console.ReadKey().KeyChar.ToString();
                Console.Clear();
                try
                {
                    switch(choice)
                    {
                        case "1":
                            InsertNewVehicle(i_Garage);
                            break;
                        case "2":
                            DisplayAllVehiclesLicenseNumberInTheGarage(i_Garage);
                            break;
                        case "3":
                            UpdateAVehicleSituation(i_Garage);
                            break;
                        case "4":
                            InflateAirOfWheelsInAVehicle(i_Garage);
                            break;
                        case "5":
                            RefuelAVehicle(i_Garage);
                            break;
                        case "6":
                            ChargeAVehicle(i_Garage);
                            break;
                        case "7":
                            DisplayASpecificVehicleDetails(i_Garage);
                            break;
                        case "8":
                            Console.WriteLine("GoodBye!");
                            Environment.Exit(0);
                            break;
                        default:
                            throw new FormatException("Invalid choice");
                    }
                }
                catch(FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    WaitForKeyPress();
                    PrintOptions(i_Garage);
                }
            }
        }

        public static void WaitForKeyPress()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Menu(Garage i_Garage)
        {
            Console.WriteLine("\t\t\t\t\t{0}", "Welcome to the garage!");
            Console.WriteLine("\t\t\t\t\t{0}", "Press any key to continue");
            Console.ReadKey();
            Console.Clear();
            PrintOptions(i_Garage);
        }

        public static void PrintOptions(Garage i_Garage)
        {
            Console.WriteLine("{0}", "Please enter 1 for inserting a new vehicle to the garage ");
            Console.WriteLine("{0}", "Please enter 2 for displaying the list of the vehicles in the garage");
            Console.WriteLine("{0}", "Please enter 3 for updating a vehicle situation in the garage");
            Console.WriteLine("{0}", "Please enter 4 for inflating air of a vehicle wheels");
            Console.WriteLine("{0}", "Please enter 5 for refueling a vehicle in the garage");
            Console.WriteLine("{0}", "Please enter 6 for charging a vehicle in the garage");
            Console.WriteLine("{0}", "Please enter 7 for displaying a specific vehicle's details in the garage");
            Console.WriteLine("{0}", "Please enter 8 for exit");
            UserInput(i_Garage);
        }

        public static void AppendWheelsDetails(StringBuilder i_StringBuilder, DetailsOfVehicleInGarage i_Vehicle)
        {
            int i = 1;

            i_StringBuilder.AppendLine("Wheels Details:").AppendLine();
            foreach (Wheel wheel in i_Vehicle.VehicleInGarage.Wheels)
            {
                i_StringBuilder.AppendFormat("Wheel number {0}", i++).AppendLine()
                    .AppendFormat("Name of Manufacturer: {0}", wheel.NameOfManufacturer).AppendLine()
                    .AppendFormat("Max air pressure: {0}", wheel.MaxAirPressure).AppendLine()
                    .AppendFormat("Current air pressure: {0}", wheel.CurrentAirPressure).AppendLine().AppendLine();
            }
        }

        public static void AppendEngineDetails(StringBuilder i_StringBuilder, DetailsOfVehicleInGarage i_vehicle)
        {
            i_StringBuilder.AppendFormat("Engine type: {0}", i_vehicle.VehicleInGarage.Engine.GetType().Name).AppendLine();
            if (i_vehicle.VehicleInGarage.Engine is Fuel)
            {
                i_StringBuilder.AppendFormat("Type of fuel: {0}", (i_vehicle.VehicleInGarage.Engine as Fuel).eType.ToString()).AppendLine()
                    .AppendFormat("Current amount of fuel: {0}", (i_vehicle.VehicleInGarage.Engine as Fuel).CurrentAmountOfFuel).AppendLine()
                    .AppendFormat("Maximum amount of fuel: {0}", (i_vehicle.VehicleInGarage.Engine as Fuel).MaxAmountOfFuel).AppendLine()
                    .AppendFormat("Percentage left: {0}", i_vehicle.VehicleInGarage.PercentageOfEnergyLeft).AppendLine();
            }
            else
            {
                i_StringBuilder.AppendFormat("Current battery staus:{0} hours left", (i_vehicle.VehicleInGarage.Engine as Electric).RemainingBatteryTime).AppendLine()
                    .AppendFormat("Maximum battery time :{0} hours", (i_vehicle.VehicleInGarage.Engine as Electric).MaxBatteryTime).AppendLine()
                    .AppendFormat("Percentage left: {0}%", i_vehicle.VehicleInGarage.PercentageOfEnergyLeft).AppendLine();
            }
        }

        public static void AppendOtherDetails(StringBuilder i_StringBuilder, DetailsOfVehicleInGarage i_Vehicle)
        {
            if (i_Vehicle.VehicleInGarage is Car)
            {
                i_StringBuilder.AppendFormat("Car color is {0}", (i_Vehicle.VehicleInGarage as Car).Color.ToString())
                    .AppendLine().AppendFormat("Number of doors: {0}", (i_Vehicle.VehicleInGarage as Car).NumberOfDoors.ToString()).AppendLine().AppendLine();
            }
            else if (i_Vehicle.VehicleInGarage is Truck)
            {
                i_StringBuilder.AppendFormat("Is driving dangerous materials:{0}", (i_Vehicle.VehicleInGarage as Truck).IsDrivingDangerousMaterials.ToString()).AppendLine()
                    .AppendFormat("Maximum carrying weight:{0}", (i_Vehicle.VehicleInGarage as Truck).MaximumCarryingWeight).AppendLine().AppendLine();
            }
            else
            {
                i_StringBuilder
                    .AppendFormat("Engine capacity:{0}", (i_Vehicle.VehicleInGarage as MotorBike).EngineCapacity)
                    .AppendLine().AppendFormat("Type of license: {0}", (i_Vehicle.VehicleInGarage as MotorBike).LicenseType.ToString()).AppendLine().AppendLine();
            }
        }

        public static void DisplayASpecificVehicleDetails(Garage i_Garage)
        {
            if (i_Garage.Details.Count == 0)
            {
                Console.WriteLine("No cars in the garage yet");
                return;
            }

            string licenseNumber = OnlyDigitsInput("Please enter the license number of the vehicle");
            Console.Clear();
            try
            {
                DetailsOfVehicleInGarage vehicle = i_Garage.Details.Find(i_Vehicle => i_Vehicle.VehicleInGarage.LicenseNumber == licenseNumber);
                StringBuilder sb = new StringBuilder().AppendLine("Vehicle Details:").AppendLine()
                    .AppendFormat("Vehicle Type: {0}", vehicle.VehicleInGarage.GetType().Name).AppendLine()
                    .AppendFormat("License Number: {0}", vehicle.VehicleInGarage.LicenseNumber).AppendLine()
                    .AppendFormat("Model Name: {0}", vehicle.VehicleInGarage.ModelName).AppendLine()
                    .AppendFormat("Owner Name: {0}", vehicle.OwnerName).AppendLine()
                    .AppendFormat("Current Situation: {0}", vehicle.CurrentSituation.ToString()).AppendLine().AppendLine().AppendLine();

                AppendEngineDetails(sb, vehicle);
                AppendOtherDetails(sb, vehicle);
                AppendWheelsDetails(sb, vehicle);

                Console.WriteLine(sb);
            }
            catch (NullReferenceException)
            {
                Console.Clear();
                printNotFound(licenseNumber);
            }
        }

        public static void ChargeAVehicle(Garage i_Garage)
        {
            if (i_Garage.Details.Count == 0)
            {
                Console.WriteLine("No cars in the garage yet");
                return;
            }

            string licenseNumber = string.Empty;

            try
            {
                Console.WriteLine("Please enter the amount of the time for charging the vehicle: ");
                float amountOfMinutesToCharge = float.Parse(Console.ReadLine());
                Console.Clear();
                licenseNumber = OnlyDigitsInput("Please enter license number:");
                Console.Clear();
                DetailsOfVehicleInGarage vehicle = i_Garage.Details.Find(i_Vehicle => i_Vehicle.VehicleInGarage.LicenseNumber == licenseNumber);
                if (vehicle.VehicleInGarage.Engine is Fuel)
                {
                    throw new ArgumentException("You cant charge a non electric car!");
                }

                i_Garage.Charge(amountOfMinutesToCharge / 60, vehicle);
                Console.WriteLine("Charging completed!");
            }
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }
            catch (NullReferenceException)
            {
                Console.Clear();
                printNotFound(licenseNumber);
            }
            catch (FormatException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }
        }

        public static eTypeOfFuels TypeOfFuelInput(string i_Message)
        {
            bool isValid = false;
            eTypeOfFuels typeOfFuel = 0;
            do
            {
                Console.WriteLine(i_Message);
                PrintTypesOfFuel();
                try
                {
                    string type = Console.ReadKey().KeyChar.ToString();
                    Console.Clear();
                    int choice = int.Parse(type);
                    if (choice > 3)
                    {
                        throw new ValueOutOfRangeException(0, 3);
                    }

                    isValid = true;
                    typeOfFuel = (eTypeOfFuels)Convert.ToInt32(type);
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            return typeOfFuel;
        }

        public static void RefuelAVehicle(Garage i_Garage)
        {
            if (i_Garage.Details.Count == 0)
            {
                Console.WriteLine("No cars in the garage yet");
                return;
            }

            eTypeOfFuels typeOfFuel = TypeOfFuelInput("Please enter type of fuel:");
            Console.Clear();
            float fuelToAdd = ValidateFloatInput("Please enter amount of fuel to add:");
            Console.Clear();
            string licenseNumber = OnlyDigitsInput("Please enter license number:");
            Console.Clear();
            try
            {
                DetailsOfVehicleInGarage vehicle = i_Garage.Details.Find(i_Vehicle => i_Vehicle.VehicleInGarage.LicenseNumber == licenseNumber);
                if (vehicle.VehicleInGarage.Engine is Electric)
                {
                    throw new ArgumentException("You cant refuel an electric car");
                }

                i_Garage.Refuel(typeOfFuel, fuelToAdd, vehicle);
                Console.WriteLine("Refueling completed!");
            }
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }
            catch (NullReferenceException)
            {
                Console.Clear();
                printNotFound(licenseNumber);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }
        }

        public static void PrintTypesOfFuel()
        {
            foreach (int i in Enum.GetValues(typeof(eTypeOfFuels)))
            {
                Console.WriteLine($"For {Enum.GetName(typeof(eTypeOfFuels), i)} enter {i}");
            }
        }

        public static void InflateAirOfWheelsInAVehicle(Garage i_Garage)
        {
            if (i_Garage.Details.Count == 0)
            {
                Console.WriteLine("No cars in the garage yet");
                return;
            }

            string licenseNumber = string.Empty;

            try
            {
                Console.WriteLine("Please enter the number license of the vehicle:");
                licenseNumber = Console.ReadLine();
                DetailsOfVehicleInGarage vehicle = i_Garage.Details.Find(i_Vehicle => i_Vehicle.VehicleInGarage.LicenseNumber == licenseNumber);
                i_Garage.InflateWheelsAirPressureToMaximum(vehicle);
                Console.WriteLine("Inflating completed!");
            }
            catch (NullReferenceException)
            {
                Console.Clear();
                printNotFound(licenseNumber);
            }
        }

        public static void PrintSituationsOfVehicle()
        {
            foreach (int i in Enum.GetValues(typeof(eCurrentSituation)))
            {
                Console.WriteLine($"For {Enum.GetName(typeof(eCurrentSituation), i)} enter {i}");
            }
        }

        public static eCurrentSituation CurrentSituationInput(string i_Message)
        {
            bool isValid = false;
            eCurrentSituation currentSituation = 0;

            do
            {
                Console.WriteLine(i_Message);
                PrintSituationsOfVehicle();

                try
                {
                    string type = Console.ReadKey().KeyChar.ToString();
                    Console.Clear();
                    int choice = int.Parse(type);
                    if (choice > 2)
                    {
                        throw new ValueOutOfRangeException(0, 3);
                    }

                    isValid = true;
                    currentSituation = (eCurrentSituation)Convert.ToInt32(type);
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            return currentSituation;
        }

        public static void UpdateAVehicleSituation(Garage i_Garage)
        {
            if (i_Garage.Details.Count == 0)
            {
                Console.WriteLine("No cars in the garage yet");
                return;
            }

            string licenseNumber = OnlyDigitsInput("Please enter license number:");
            Console.Clear();
            eCurrentSituation currentSituation = CurrentSituationInput("Please choose a situation of vehicle:");

            try
            {
                DetailsOfVehicleInGarage vehicle = i_Garage.Details.Find(i_Vehicle => i_Vehicle.VehicleInGarage.LicenseNumber == licenseNumber);
                i_Garage.ChangeCurrentSituationOfVehicle(currentSituation, vehicle);
                Console.WriteLine("The car situation has been changed!");
            }
            catch (NullReferenceException)
            {
                Console.Clear();
                printNotFound(licenseNumber);
            }
        }

        public static void PrintWithFilter(Garage i_Garage)
        {
            int i = 1;
            eCurrentSituation currentSituation = CurrentSituationInput("Please choose a situation of a vehicle as a filter:");
            foreach (DetailsOfVehicleInGarage vehicle in i_Garage.Details)
            {
                if (vehicle.CurrentSituation == currentSituation)
                {
                    Console.WriteLine("License number #{0} is {1}", i, vehicle.VehicleInGarage.LicenseNumber);
                    i++;
                }
            }
        }

        public static void PrintWithOutFilter(Garage i_Garage)
        {
            int i = 1;
            foreach (DetailsOfVehicleInGarage vehicle in i_Garage.Details)
            {
                Console.WriteLine("License number #{0} is {1}", i, vehicle.VehicleInGarage.LicenseNumber);
                i++;
            }
        }

        public static void DisplayAllVehiclesLicenseNumberInTheGarage(Garage i_Garage)
        {
            if (i_Garage.Details.Count == 0)
            {
                Console.WriteLine("No cars in the garage yet");
                return;
            }

            Console.WriteLine("Please enter 1 to view all the license Numbers with a situation filter or 0 without a filter:");
            try
            {
                string input = Console.ReadKey().KeyChar.ToString();
                Console.Clear();
                int withFilter = int.Parse(input);
                if (withFilter > 1)
                {
                    throw new ValueOutOfRangeException(0, 1);
                }

                if (withFilter == 1)
                {
                    PrintWithFilter(i_Garage);
                }
                else
                {
                    PrintWithOutFilter(i_Garage);
                }
            }
            catch (FormatException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }
        }

        public static string TypeOfVehicleInput(string i_Message)
        {
            string input = string.Empty;
            bool isValid = false;
            do
            {
                Console.WriteLine(i_Message);
                PrintTypesOfVehicles();
                try
                {
                    input = Console.ReadKey().KeyChar.ToString();
                    Console.Clear();
                    int choice = int.Parse(input);
                    if (choice > 2)
                    {
                        throw new ValueOutOfRangeException(0, 2);
                    }

                    isValid = true;
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);
            return input;
        }

        public static string OnlyLettersInput(string i_Message)
        {
            bool isValid = false;
            string input = string.Empty;
            Regex regex = new Regex("^[a-zA-Z ]");

            do
            {
                Console.WriteLine(i_Message);
                try
                {
                    input = Console.ReadLine();
                    if (!regex.IsMatch(input))
                    {
                        throw new FormatException("The string must contain only a letters");
                    }

                    isValid = true;
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            return input;
        }

        public static string OnlyDigitsInput(string i_Message)
        {
            bool isValid = false;
            string input = string.Empty;
            Regex regex = new Regex("^[0-9]+$");

            do
            {
                Console.WriteLine(i_Message);
                try
                {
                    input = Console.ReadLine();
                    if (!regex.IsMatch(input))
                    {
                        throw new FormatException("The string must contain only digits");
                    }

                    isValid = true;
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            return input;
        }

        public static bool CheckIfAlreadyExist(Garage i_Garage, string i_LicenseNumber)
        {
            bool isExist = false;
            DetailsOfVehicleInGarage vehicle = i_Garage.Details.Find(i_Vehicle => i_Vehicle.VehicleInGarage.LicenseNumber == i_LicenseNumber);
            if (vehicle != null)
            {
                isExist = true;
                vehicle.CurrentSituation = eCurrentSituation.InRepair;
            }

            return isExist;
        }
        public static void InsertNewVehicle(Garage i_Garage)
        {
            string licenseNumber = OnlyDigitsInput("Please enter the license number of the vehicle:");
            Console.Clear();
            bool isAlreadyExist = CheckIfAlreadyExist(i_Garage, licenseNumber);

            if (isAlreadyExist)
            {
                Console.Clear();
                Console.WriteLine("this number license is already in the garage!");
                return;
            }

            string typeOfVehicle = TypeOfVehicleInput("Please choose the type of vehicle that you want to add to the garage:");
            Console.Clear();
            string ownerName = OnlyLettersInput("Please enter the name of the owner:");
            Console.Clear();
            string ownerPhoneNumber = OnlyDigitsInput("Please enter the phone number of the owner:");
            Console.Clear();
            string modelName = OnlyLettersInput("Please enter the model name:");
            Console.Clear();

            switch (typeOfVehicle)
            {
                case "0":
                    CreateTruck(i_Garage, ownerName, ownerPhoneNumber, modelName, licenseNumber);
                    break;
                case "1":
                    CreateCar(i_Garage, ownerName, ownerPhoneNumber, modelName, licenseNumber);
                    break;
                case "2":
                    CreateMotorBike(i_Garage, ownerName, ownerPhoneNumber, modelName, licenseNumber);
                    break;
            }
        }

        public static void PrintTypesOfVehicles()
        {
            foreach (int i in Enum.GetValues(typeof(eTypeOfVehicles)))
            {
                Console.WriteLine($"For {Enum.GetName(typeof(eTypeOfVehicles), i)} enter {i}");
            }
        }

        public static void PrintTypesOfLicenses()
        {
            foreach (int i in Enum.GetValues(typeof(eLicenseTypes)))
            {
                Console.WriteLine($"For {Enum.GetName(typeof(eLicenseTypes), i)} enter {i}");
            }
        }

        public static void CreateMotorBike(Garage i_Garage, string i_OwnerName, string i_OwnerPhoneNumber, string i_ModelName, string i_LicenseNumber)
        {
            int engineCapacity = engineCapacityInput("Please enter the engine capacity:");
            Console.Clear();
            eLicenseTypes licenseType = LicenseTypeInput("please choose the license type:");
            Console.Clear();
            IsElectricInput("please choose 1 for electric motorBike or 0 for regular motorBike:", out bool isElectric);
            MotorBike motorBike = i_Garage.VehiclesCreator.CreateMotorBike(i_ModelName, i_LicenseNumber, engineCapacity, licenseType, isElectric);
            i_Garage.InsertVehicle(i_OwnerName, i_OwnerPhoneNumber, motorBike);
            Console.WriteLine("The motorbike has been created!");
        }

        public static void IsElectricInput(string i_Message, out bool o_IsElectric)
        {
            bool isValid = false;
            string input = string.Empty;

            do
            {
                Console.WriteLine(i_Message);
                try
                {
                    input = Console.ReadKey().KeyChar.ToString();
                    Console.Clear();
                    int choice = int.Parse(input);
                    if (choice > 1)
                    {
                        throw new ValueOutOfRangeException(0, 1);
                    }

                    isValid = true;
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            o_IsElectric = input.Equals("1");
        }

        public static eLicenseTypes LicenseTypeInput(string i_Message)
        {
            bool isValid = false;
            eLicenseTypes licenseType = 0;

            do
            {
                Console.WriteLine(i_Message);
                PrintTypesOfLicenses();
                try
                {
                    string input = Console.ReadKey().KeyChar.ToString();
                    Console.Clear();
                    int type = int.Parse(input);
                    if (type > 3)
                    {
                        throw new ValueOutOfRangeException(0, 3);
                    }

                    licenseType = (eLicenseTypes)Convert.ToInt32(input);
                    isValid = true;
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            return licenseType;
        }

        public static void PrintTypesOfColors()
        {
            foreach (int i in Enum.GetValues(typeof(eColors)))
            {
                Console.WriteLine($"For {Enum.GetName(typeof(eColors), i)} enter {i}");
            }
        }

        public static void PrintAvailableAmountOfOfDoors()
        {
            foreach (int i in Enum.GetValues(typeof(eDoors)))
            {
                Console.WriteLine($"For {Enum.GetName(typeof(eDoors), i)} enter {i}");
            }
        }

        public static eColors ColorInput(string i_Message)
        {
            bool isValid = false;
            eColors eColor = 0;

            do
            {
                Console.WriteLine(i_Message);
                PrintTypesOfColors();
                try
                {
                    string input = Console.ReadKey().KeyChar.ToString();
                    Console.Clear();
                    int choice = int.Parse(input);
                    if (choice > 3)
                    {
                        throw new ValueOutOfRangeException(0, 3);
                    }

                    isValid = true;
                    eColor = (eColors)Convert.ToInt32(input);
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            return eColor;
        }

        public static eDoors DoorsInput(string i_Message)
        {
            bool isValid = false;
            eDoors doors = 0;

            do
            {
                Console.WriteLine(i_Message);
                PrintAvailableAmountOfOfDoors();
                try
                {
                    string input = Console.ReadKey().KeyChar.ToString();
                    Console.Clear();
                    int choice = int.Parse(input);
                    if (choice > 3)
                    {
                        throw new ValueOutOfRangeException(0, 3);
                    }

                    isValid = true;
                    doors = (eDoors)Convert.ToInt32(input);
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            return doors;
        }

        public static void CreateCar(Garage i_Garage, string i_OwnerName, string i_OwnerPhoneNumber, string i_ModelName, string i_LicenseNumber)
        {
            IsElectricInput("please choose 1 for an electric car or 0 for a regular car:", out bool isElectric);
            eColors color = ColorInput("Please choose color:");
            eDoors numberOfDoors = DoorsInput("Please choose number of doors");
            Car car = i_Garage.VehiclesCreator.CreateCar(i_ModelName, i_LicenseNumber, numberOfDoors, color, isElectric);
            i_Garage.InsertVehicle(i_OwnerName, i_OwnerPhoneNumber, car);
            Console.WriteLine("The car has been created!");
        }

        public static float ValidateFloatInput(string i_Message)
        {
            bool isValid = false;
            float input = 0;

            do
            {
                Console.WriteLine(i_Message);
                try
                {
                    input = float.Parse(Console.ReadLine());
                    isValid = true;
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            return input;
        }

        public static void IsDrivingDangerousMaterialsInput(string i_Message, out bool o_Flag)
        {
            bool isValid = false;
            int choice = 0;

            do
            {
                Console.WriteLine(i_Message);
                try
                {
                    string input = Console.ReadKey().KeyChar.ToString();
                    choice = int.Parse(input);
                    if (choice > 1)
                    {
                        throw new ValueOutOfRangeException(0, 1);
                    }

                    isValid = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            o_Flag = choice.Equals(1);
        }

        public static void CreateTruck(Garage i_Garage, string i_OwnerName, string i_OwnerPhoneNumber, string i_ModelName, string i_LicenseNumber)
        {
            float maximumCarryingWeight = ValidateFloatInput("Please enter the maximum carry weight of the truck:");
            Console.Clear();
            IsDrivingDangerousMaterialsInput("Please enter 0 for driving regular materials and 1 for driving dangerous materials:", out bool flag);
            Console.Clear();
            Truck truck = i_Garage.VehiclesCreator.CreateTruck(i_ModelName, i_LicenseNumber, flag, maximumCarryingWeight);
            i_Garage.InsertVehicle(i_OwnerName, i_OwnerPhoneNumber, truck);
            Console.WriteLine("The truck has been created!");
        }

        private static int engineCapacityInput(string i_Message)
        {
            bool isValid = false;
            int result = 0;
            do
            {
                Console.WriteLine(i_Message);
                try
                {
                    result = int.Parse(Console.ReadLine());
                    isValid = true;
                }
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isValid);

            return result;
        }

        private static void printNotFound(string i_LicenseNumber)
        {
            Console.WriteLine("Vehicle with the license number {0} not found.", i_LicenseNumber);
        }
    }
}