using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GrarageLogic.Fuel;
using static Ex03.GrarageLogic.Motorcycle;

namespace Ex03.GrarageLogic
{
    public class Truck : Vehicle
    {
        protected const float k_MaxTirePressure = 28f;
        protected const int k_NumberOfTires = 12;

        public const eFuelType k_FuelType = eFuelType.Soler;
        public const float k_MaxFuelAmount = 110f;

        private bool m_IsTransportingHazardousMaterials;
        private float m_CargoVolume;

        public Truck(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            for (int i = 0; i < k_NumberOfTires; i++)
            {
                m_Tires.Add(new Tire(k_MaxTirePressure));
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                if (value >= k_MinVolumeAmount)
                {
                    m_CargoVolume = value;
                }
                else
                {
                    throw new ArgumentException(string.Format("Cargo Volume must be greater than {0}.", k_MinVolumeAmount));
                }
            }
        }

        public bool IsTransportingHazardousMaterials
        {
            get
            {
                return m_IsTransportingHazardousMaterials;
            }
            set
            {
                m_IsTransportingHazardousMaterials = value;
            }
        }

        public override void ExtendToString(StringBuilder i_Sb)
        {
            i_Sb.AppendFormat("Cargo Voulume: {0} {1}", m_CargoVolume, k_VolumeUnits);
            i_Sb.AppendLine();
            i_Sb.AppendFormat((m_IsTransportingHazardousMaterials ? "T" : "Not t"));
            i_Sb.AppendFormat("ransporting hazardous materials");
            i_Sb.AppendLine();
            i_Sb.AppendFormat("There are {0} Tires, the maximum air pressure of each Tire is {1}: ", k_NumberOfTires, k_MaxTirePressure);
            i_Sb.AppendLine();
        }

        public override List<string> RequestSpecificVehicleDetails()
        {

            List<string> ListOfDataMembers = new List<string>();

            ListOfDataMembers.Add(string.Format("Cargo Volume (Please enter a numerical value):"));
            ListOfDataMembers.Add(string.Format("Could you let us know if your truck will be transporting hazardous materials? (Please enter 'yes' or 'no'):"));

            return ListOfDataMembers;
        }

        public override void ValidateAndSetSpecificVehicleDetails(List<string> i_UserInput)
        {
            if (int.TryParse(i_UserInput[0], out int parsedCargoVolume))
            {
                this.CargoVolume = parsedCargoVolume;
            }
            else
            {
                throw new FormatException("Cargo Volume must be a number!");
            }
            string secondUserInput = i_UserInput[1].ToLower();
            if (secondUserInput == "yes")
            {
                this.IsTransportingHazardousMaterials = true;
            }
            else if (secondUserInput == "no")
            {
                this.IsTransportingHazardousMaterials = false;
            }
            else
            {
                throw new ArgumentException("You must enter yes or no as an answer " +
                                            "to the question about the hazardous materials");
            }
        }
    }

}
