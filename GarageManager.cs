using Ex03.GrarageLogic;
using System;
using System.Collections.Generic;
using static Ex03.GrarageLogic.Garage;
using static Ex03.GrarageLogic.Fuel;
using static Ex03.GrarageLogic.VehicleCard;
using static Ex03.ConsoleUI.InputValidator;
using static Ex03.GrarageLogic.VehicleMaker;


namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        Garage m_Garage = new Garage();

        public enum eMenuOptions
        {
            InsertNewVehicle = 1,
            PrintLicenseNumbersSortedByStatus,
            ChangeVehicleStatus,
            InflateTiresToMax,
            RefuelVehicle,
            ChargeVehicle,
            PrintVehicleInformation,
            Exit,
        }

        public const int k_NumMenuOptions = 8;

        public void InitGarageManager()
        {
            printGarageTitle();
            bool userQuited;
            string userInput;

            try
            {
                do
                {
                    printMenu();
                    userInput = Console.ReadLine();
                    eMenuOptions choosenOption = (eMenuOptions)ParseAndValidateEnumChoice
                                                 (userInput, "menue option", k_NumMenuOptions);
                    userQuited = processUserInput(choosenOption);
                    printSeparator();

                } while (!userQuited);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            printGoodBye();
        }

        private static void printMenu()
        {
            Console.WriteLine(
    $@"Please enter a number to choose an option (enter {(int)eMenuOptions.Exit} to exit):
------------------------------------------------------------------------
({(int)eMenuOptions.InsertNewVehicle}) Insert new vehicle to the garage.
({(int)eMenuOptions.PrintLicenseNumbersSortedByStatus}) Print License numbers of veicles in garage, optional - sorted by status.
({(int)eMenuOptions.ChangeVehicleStatus}) Change status of vehicle.
({(int)eMenuOptions.InflateTiresToMax}) Inflate all tires of vehicle to maximum.
({(int)eMenuOptions.RefuelVehicle}) Refuel a vehicle.
({(int)eMenuOptions.ChargeVehicle}) Charge a vehicle.
({(int)eMenuOptions.PrintVehicleInformation}) Print vehicle information");
        }

        private bool processUserInput(eMenuOptions i_Option)
        {
            bool userQuited = false;

            switch (i_Option)
            {
                case eMenuOptions.InsertNewVehicle:
                    insertNewVehicle();
                    break;
                case eMenuOptions.PrintLicenseNumbersSortedByStatus:
                    printLicenseNumbersSortedByStatus();
                    break;
                case eMenuOptions.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eMenuOptions.InflateTiresToMax:
                    inflateTiresToMax();
                    break;
                case eMenuOptions.RefuelVehicle:
                    refuelVehicle();
                    break;
                case eMenuOptions.ChargeVehicle:
                    chargeVehicle();
                    break;
                case eMenuOptions.PrintVehicleInformation:
                    printVehicleInformation();
                    break;
                case eMenuOptions.Exit:
                    userQuited = true;
                    break;
                default:
                    throw new ArgumentException("Invalid input!");
            }

            return userQuited;
        }

        private void insertNewVehicle()
        {
            string licenseNumber;
            licenseNumber = getLicenseNumberFromUser();

            if (m_Garage.IsVehicleInGarage(licenseNumber))
            {
                Console.WriteLine("Vehicle already in garage.");
            }
            else
            {
                addNewVechicleCard(licenseNumber);
            }
        }

        private void addNewVechicleCard(string i_licenseNumber)
        {
            try
            {
                VehicleCard vehicleCard;
                vehicleCard = createVehicleCard(i_licenseNumber);

                if (vehicleCard != null)
                {
                    m_Garage.AddNewVehicleCard(i_licenseNumber, vehicleCard);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void changeVehicleStatus()
        {
            if (!notifyUserGarageEmpty())
            {
                string licenseNumber = getLicenseNumberFromUser();
                
                if (!notifyLicenseNumberNotFoundInGarage(licenseNumber))
                {
                    printVehicleStatus();
                    string enumChoiceStr = Console.ReadLine();
                    
                    try
                    {
                        eVehicleStatus vehicleStatus = (eVehicleStatus)ParseAndValidateEnumChoice
                            (enumChoiceStr, "vehicle status", VehicleCard.k_VehicleStatusSize);
                        m_Garage.ChangeVehicleStatus(licenseNumber, vehicleStatus);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            }
        }

        private void chargeVehicle()
        {
            if (!notifyUserGarageEmpty())
            {
                string licenseNumber = getLicenseNumberFromUser();

                if (!notifyLicenseNumberNotFoundInGarage(licenseNumber))
                {
                    Console.WriteLine("Please insert minutes to charge: ");
                    string minutesToChargeStr = Console.ReadLine();

                    try
                    {
                        float minutesToCharge = ParseAndValidateFloat(minutesToChargeStr, "minutes");
                        float hoursToAdd = minutesToCharge / 60;
                        m_Garage.AddEnergy(licenseNumber, hoursToAdd, null);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }

            }
        }

        private void refuelVehicle()
        {
            if (!notifyUserGarageEmpty())
            {
                string licenseNumber = getLicenseNumberFromUser();

                if (!notifyLicenseNumberNotFoundInGarage(licenseNumber))
                {
                    try
                    {
                        Console.WriteLine("Please insert amout of fuel to add: ");
                        string fuelToAddStr = Console.ReadLine();
                        printFuelTypes();
                        string enumChoiceStr = Console.ReadLine();
                        float fuelToAdd = ParseAndValidateFloat(fuelToAddStr, "fuel");
                        eFuelType fuelType = (eFuelType)ParseAndValidateEnumChoice
                            (enumChoiceStr, "fuel type", Fuel.k_NumOfFuelType);

                        m_Garage.AddEnergy(licenseNumber, fuelToAdd, fuelType);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            }
        }

        private void inflateTiresToMax()
        {
            if (!notifyUserGarageEmpty())
            {
                string licenseNumber = getLicenseNumberFromUser();

                if (!notifyLicenseNumberNotFoundInGarage(licenseNumber))
                {
                    m_Garage.InflateTiresToMax(licenseNumber);
                }
            }
        }


        private void printVehicleInformation()
        {
            if (!notifyUserGarageEmpty())
            {
                string licenseNumber = getLicenseNumberFromUser();

                if (!notifyLicenseNumberNotFoundInGarage(licenseNumber))
                {
                    Console.WriteLine(m_Garage.GetSpecificVehicleData(licenseNumber));
                }
            }
        }

        private bool notifyLicenseNumberNotFoundInGarage(string i_LicenseNumber)
        {
            bool res = false;

            if (!m_Garage.IsVehicleInGarage(i_LicenseNumber))
            {
                Console.WriteLine("License Number inserted not in the garage.");
                res = true;
            }

            return res;
        }

        private static string getLicenseNumberFromUser()
        {
            string licenseNumber;
            Console.WriteLine("Please enter a license number:");
            licenseNumber = Console.ReadLine();
            return licenseNumber;
        }

        private bool notifyUserGarageEmpty()
        {
            bool res = false;

            if (m_Garage.IsGarageEmpty())
            {
                Console.WriteLine("Notice the garage is empty, please insert new vehicle first.");
                res = true;
            }

            return res;
        }

        private void printLicenseNumbersSortedByStatus()
        {
            bool isEmptyGarage = notifyUserGarageEmpty();
            if (!isEmptyGarage)
            {
                List<string> licenseNumbers;
                int userChoise = askUserIfPrintLicenseNumbersSortedByStatus();
                if (userChoise == 1)
                {
                    printVehicleStatus();
                    string userInput = Console.ReadLine();
                    eVehicleStatus userStatusChoice = (eVehicleStatus)ParseAndValidateEnumChoice(userInput, "vehicle status", k_VehicleStatusSize);
                    licenseNumbers = m_Garage.GetVehiclesLicenseNumbersSortByStatus(userStatusChoice);
                }
                else
                {
                    licenseNumbers = m_Garage.GetVehiclesLicenseNumbersSortByStatus(null);
                }

                Console.WriteLine("License Numbers:");
                int i = 1;
                foreach (string licenseNumber in licenseNumbers)
                {
                    Console.WriteLine("{0}# {1}", i, licenseNumber);
                    i++;
                }
            }
        }

        private static void printGarageTitle()
        {
            Console.WriteLine(@"
                 ____________________________________________________
  (>_____.----'||                                                    |
   /           ||                                                    |
  |---.   = /  ||                                                    |
  |    |  ( '  ||            Welcome To Our Garage                   |
  |    |   `   ||                                                    |
  |---'        ||                                                    |
  |            ||                                                    |
  [    ________||____________________________________________________|
  [__.'.---.   |[Y__Y__Y__Y__Y__Y__Y__Y__Y__Y__Y__Y__Y__Y__Y__Y__Y__Y]
  [   //.-.\\__| `.__//.-.\\//.-.\\_________________//.-.\\//.-.\\_.'\
  [__/( ( ) )`      '( ( ) )( ( ) )`               '( ( ) )( ( ) )`
-------`---'----------`---'--`---'-------------------`---'--`---'------

-----------------------------------------------------------------------

");
        }

        private static void printSeparator()
        {
            Console.WriteLine(@"
       i     ______
       |   /  |    |\
  _____|_/____|____|  \              
 /      |    -|        |            
{_______|_____|_______/====================================================================
   \__/         \__/                

");
        }

        private static void printGoodBye()
        {
            Console.WriteLine(@"
                                       ____.----'_     --
                                    .'',           ',
                                   /    ,            '       ---
                                   @> @>'            _.'
                                  | <   ')  _.----_-'
                                  \\__/ ,.-'    (( )     --
                                   \_.- <     .'\ /
                              ____   )   \_.-'   >
                          .-'""   `""--._        .'    ----
                         / |  _   / //|    _.-'
                        /  |(_(.\/ // |    |
                      _/_  |J   L //  |    |----'""""""""`._    -   --
      ______.------'""""   `""""""""---' `--'-------.-------'--""--._
    .-.__            ___.----"""""" +-------- == .-------------. L
   L _    ""`--. .-""""'                                         |  --
  ( ( ) // , , F/_/ .----.       |            '     .---.     F
  |"" ""  V ( ( )    / .--. `.     '            |    /.-.\ \   C>
  <`--._   "" ""|   // .--.`. \    +            '   //   \\ L--'  -
   `--.__`-----. // /    \ L L   .            '  / F  ) H_|          --
     `--._`---""_/J J   \  L| \___.__.-----,----""""J \   //      ---
       \  `""""""/`-| |   /  F| |""        `""'        \ `-'/
        `----'   J \     / F-'        VK           `""""'
                  \ `-.-'.'
                   `""---'

                                 Thank you for using our system
---------------------------------------------------------------------------------------------------



");
        }

        private static void printEnum(System.Type i_TypeOfEnum, int i_EnumSize)
        {
            string memberInEnum;
            for (int i = 1; i <= i_EnumSize; i++)
            {
                memberInEnum = Enum.GetName(i_TypeOfEnum, i);
                Console.WriteLine("{0}.{1}", i, memberInEnum);
            }

        }

        private static void printVehicleTypes()
        {
            Console.WriteLine();
            Console.WriteLine("Choose a vehicle type: ");
            int i = 1;

            foreach (String vehicleType in VehicleMaker.sr_VehicleTypes)
            {
                Console.WriteLine("{0}.{1}", i, vehicleType);
                i++;
            }

            Console.WriteLine("Enter the corresponding number for your vehicle type:");
            Console.WriteLine();
        }


        private static VehicleCard createVehicleCard(string i_LicenseNumber)
        {
            VehicleCard vehicleCard = null;

            vehicleCard = new VehicleCard();
            Console.WriteLine("Owner Name: ");
            vehicleCard.OwnerName = Console.ReadLine();
            Console.WriteLine("Owner Number: ");
            vehicleCard.OwnerName = Console.ReadLine();
            vehicleCard.Vehicle = getVehicleDetailsFromUser(i_LicenseNumber);

            return vehicleCard;
        }

        private static Vehicle getVehicleDetailsFromUser(string i_LicenseNumber)
        {
            printVehicleTypes();
            string VehicleTypesChoice = Console.ReadLine();
            eVehicleOptions VehicleType = (eVehicleOptions)ParseAndValidateEnumChoice(
                VehicleTypesChoice, "Vehicle type choice", (int)k_NumVehicleOptions);
            Vehicle vehicle = MakeVehicle(i_LicenseNumber, VehicleType);
            Console.WriteLine("Model Name: ");
            vehicle.ModelName = Console.ReadLine();
            getAndSetSpecificVehicleTypeDetails(vehicle);
            getEnergyResourceDetailsFromUser(vehicle.Engine);
            getTiresDetailsFromUser(vehicle.Tires);
            return vehicle;
        }

        private static void getEnergyResourceDetailsFromUser(EnergyResource io_egnine)
        {
            bool validInput = false;
            Console.WriteLine("Current amount of energy: ");
            do
            {
                try
                {
                    string userInput = Console.ReadLine();
                    io_egnine.CurrentAmount = ParseAndValidateFloat(userInput, "amount of energy");
                    validInput = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Please enter again: ");

                }
            } while (!validInput);
        }

        private static void getTiresDetailsFromUser(List<Tire> io_Tires)
        {
            int i = 1;
            bool IsAllTiresTheSame = askUserForSameTires();
            if (!IsAllTiresTheSame)
            {
                foreach (Tire tire in io_Tires)
                {
                    Console.WriteLine("Tire #{0}", i);
                    getOneTireDetailsFromUser(tire);
                    i++;
                }
            }
            else
            {
                getOneTireDetailsFromUser(io_Tires[0]);
                foreach (Tire tire in io_Tires)
                {
                    tire.CurrentAirPressure = io_Tires[0].CurrentAirPressure;
                    tire.ManufacturerName = io_Tires[0].ManufacturerName;
                }
            }
        }

        private static bool askUserForSameTires()
        {
            Console.WriteLine("Would you like all tires to be the same?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            while (true)
            {
                Console.Write("Enter the corresponding number (1 or 2): ");
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    return true;
                }
                else if (userInput == "2")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1 for Yes or 2 for No.");
                }
            }
        }

        private static void getOneTireDetailsFromUser(Tire io_Tire)
        {
            bool validInput = false;
            Console.WriteLine("Manufacturer Name: ");
            io_Tire.ManufacturerName = Console.ReadLine();
            do
            {
                try
                {
                    Console.WriteLine("Current Air Pressure: ");
                    string userInput = Console.ReadLine();
                    io_Tire.CurrentAirPressure = ParseAndValidateFloat(userInput, "Air Pressure");
                    validInput = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Please insert again:");
                }

            } while (!validInput);
        }

        private static void printFuelTypes()
        {
            Console.WriteLine("Choose a fuel type: ");
            printEnum(typeof(eFuelType), k_NumOfFuelType);
            Console.WriteLine("Enter the corresponding number for your choice:");
        }

        private static void printVehicleStatus()
        {
            Console.WriteLine("Choose a vehicle status ");
            printEnum(typeof(eVehicleStatus), k_VehicleStatusSize);
            Console.WriteLine("Enter the corresponding number for your choice:");
        }

        private static int askUserIfPrintLicenseNumbersSortedByStatus()
        {
            Console.WriteLine("Do you want to print License Numbers Sorted By Status or all the License numbers?");
            Console.WriteLine("1. Print License Numbers Sorted By Status");
            Console.WriteLine("2. Print all the License numbers");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
            }

            return choice;
        }

        private static void getAndSetSpecificVehicleTypeDetails(Vehicle io_vehicle)
        {
            List<string> vehicleTypeDetails = new List<string>();
            List<string> userInputsList = new List<string>();
            string userInput;

            vehicleTypeDetails = io_vehicle.RequestSpecificVehicleDetails();
            foreach (string vehicleTypeDetail in vehicleTypeDetails)
            {
                Console.WriteLine($"{vehicleTypeDetail} ");
                userInput = Console.ReadLine();
                userInputsList.Add(userInput);
            }

            io_vehicle.ValidateAndSetSpecificVehicleDetails(userInputsList);
        }
    }
}
