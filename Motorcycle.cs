using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GrarageLogic.Car;
using static Ex03.GrarageLogic.Fuel;

namespace Ex03.GrarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A1 = 1,
            A2,
            AB,
            B2,
        }

        protected const float k_MaxTirePressure = 29f;
        protected const int k_NumberOfTires = 2;
        protected const int k_NumOfLicenseTypes = 4;

        public const eFuelType k_FuelType = eFuelType.Octan98;
        public const float k_MaxFuelAmount = 5.8f;
        public const float k_MaxBatteryTimeInHours = 2.8f;

        private eLicenseType m_LicenseType;
        private float m_EngineVolume;

        public Motorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            for (int i = 0; i < k_NumberOfTires; i++)
            {
                m_Tires.Add(new Tire(k_MaxTirePressure));
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                if (value < eLicenseType.A1 || value > eLicenseType.B2)
                {
                    throw new ValueOutOfRangeException((int)eLicenseType.A1, (int)eLicenseType.B2);
                }
                else
                {
                    m_LicenseType = value;
                }
            }
        }

        public float EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                if (value < k_MinVolumeAmount)
                {
                    throw new ArgumentException(
                        string.Format("Engine Volume must be greater than {0}.", k_MinVolumeAmount));
                }
                else
                {
                    m_EngineVolume = value;
                }
            }
        }

        public static float MaxBatteryTimeInHours
        {
            get
            {
                return k_MaxBatteryTimeInHours;
            }
        }

        public override void ExtendToString(StringBuilder io_Sb)
        {
            io_Sb.AppendFormat("License Type: {0}", m_LicenseType);
            io_Sb.AppendLine();
            io_Sb.AppendFormat("Engine Volume: {0} {1}", m_EngineVolume, k_VolumeUnits);
            io_Sb.AppendLine();
            io_Sb.AppendFormat("There are {0} Tires, the maximum air pressure of each Tire is {1}: ", k_NumberOfTires, k_MaxTirePressure);
            io_Sb.AppendLine();
        }

        public override List<string> RequestSpecificVehicleDetails()
        {

            List<string> ListOfDataMembers = new List<string>();

            ListOfDataMembers.Add(string.Format("Engine Volume (Please enter a numerical value):"));
            ListOfDataMembers.Add(string.Format(
$@"Choose a license type: 
1.{eLicenseType.A1}
2.{eLicenseType.A2}
3.{eLicenseType.AB}
4.{eLicenseType.B2}
Enter the corresponding number for your license:"));

            return ListOfDataMembers;
        }

        public override void ValidateAndSetSpecificVehicleDetails(List<string> i_UserInput)
        {
            if (int.TryParse(i_UserInput[0], out int parsedEngineVolume))
            {
                this.EngineVolume = parsedEngineVolume;
            }
            else
            {
                throw new FormatException("Engine Volume must be a number!");
            }
            if (int.TryParse(i_UserInput[1], out int parsedLicenseTypeChoice)
                && (int)(eLicenseType.A1) <= parsedLicenseTypeChoice && k_NumOfLicenseTypes >= parsedLicenseTypeChoice)
            {
                this.LicenseType = (eLicenseType)parsedLicenseTypeChoice;
            }
            else
            {
                throw new ValueOutOfRangeException((float)(eLicenseType.A1), (float)k_NumOfLicenseTypes);
            }
        }
    }
}

