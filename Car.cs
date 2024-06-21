using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GrarageLogic.Fuel;
using static Ex03.GrarageLogic.VehicleCard;


namespace Ex03.GrarageLogic
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Blue = 1,
            White,
            Red,
            Yellow,
        }

        protected const float k_MaxTirePressure = 30f;
        protected const int k_NumberOfTires = 5;
        protected const int k_MaxNumberOfDoors = 5;
        protected const int k_MinNumberOfDoors = 2;
        protected const int k_NumOfOptionsForColor = 4;

        public const eFuelType k_FuelType = eFuelType.Octan95;
        public const float k_MaxFuelAmount = 58f;
        public const float k_MaxBatteryTimeInHours = 4.8f;

        private eColor m_Color;
        private int m_NumberOfDoors;

        public Car(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            for (int i = 0; i < k_NumberOfTires; i++)
            {
                m_Tires.Add(new Tire(k_MaxTirePressure));
            }
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                if (value < eColor.Blue || value > eColor.Yellow)
                {
                    throw new ValueOutOfRangeException((float)eColor.Blue, (float)eColor.Yellow);
                }
                else
                {
                    m_Color = value;
                }
            }
        }

        public int NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }
            set
            {
                if (value < k_MinNumberOfDoors || value > k_MaxNumberOfDoors)
                {
                    throw new ValueOutOfRangeException(k_MinNumberOfDoors, k_MaxNumberOfDoors);
                }
                else
                {
                    m_NumberOfDoors = value;
                }
            }
        }

        public override void ExtendToString(StringBuilder io_Sb)
        {
            io_Sb.AppendFormat("Color: {0}", m_Color);
            io_Sb.AppendLine();
            io_Sb.AppendFormat("Number of doors: {0}", m_NumberOfDoors);
            io_Sb.AppendLine();
            io_Sb.AppendFormat("There are {0} Tires, the maximum air pressure of each Tire is {1}: ", k_NumberOfTires, k_MaxTirePressure);
            io_Sb.AppendLine();
        }

        public override List<string> RequestSpecificVehicleDetails()
        {
            List<string> ListOfDataMembers = new List<string>();

            ListOfDataMembers.Add(string.Format("Number of doors({0} - {1}):", k_MinNumberOfDoors, k_MaxNumberOfDoors));
            ListOfDataMembers.Add(string.Format(
$@"Choose a color: 
1.{eColor.Blue}
2.{eColor.White}
3.{eColor.Red}
4.{eColor.Yellow}
Enter the corresponding number for your preferred color:"));

            return ListOfDataMembers;
        }

        public override void ValidateAndSetSpecificVehicleDetails(List<string> i_UserInput)
        {
            if (int.TryParse(i_UserInput[0], out int parsedDoorsNumber))
            {
                this.NumberOfDoors = parsedDoorsNumber;
            }
            else
            {
                throw new ArgumentException("Number of doors must be an integer number!");
            }
            if (int.TryParse(i_UserInput[1], out int parsedColorChoice)
                && 1 <= parsedDoorsNumber && k_NumOfOptionsForColor >= parsedDoorsNumber)
            {
                this.Color = (eColor)parsedColorChoice;
            }
            else
            {
                throw new ArgumentException(string.Format("In order to choose a color you must " +
                                             "enter an integer number 1-{0}!", k_NumOfOptionsForColor));
            }
        }
    }

}