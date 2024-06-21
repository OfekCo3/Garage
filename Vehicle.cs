using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GrarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected readonly string m_LicenseNumber;
        protected EnergyResource m_Engine;
        protected List<Tire> m_Tires = new List<Tire>();

        public const string k_VolumeUnits = "cc";
        public const float k_MinVolumeAmount = 0;

        protected Vehicle(string i_LicenseNumber)
        {
            m_LicenseNumber = i_LicenseNumber;
        }

        public EnergyResource Engine
        {
            get
            {
                return m_Engine;
            }
            set
            {
                m_Engine = value;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
            set
            {
                m_ModelName = value;
            }
        }

        public List<Tire> Tires
        {
            get { return m_Tires; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
        }

        public override bool Equals(object i_Obj)
        {
            bool equals = false;
            Vehicle toCompareTo = i_Obj as Vehicle;
            
            if (toCompareTo != null)
            {
                equals = this.m_LicenseNumber == toCompareTo.m_LicenseNumber;
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return m_LicenseNumber.GetHashCode();
        }

        public static bool operator ==(Vehicle i_Vehicle, string i_LicenseNumber)
        {
            bool res = false;

            if (i_Vehicle != null)
            {
                res = i_Vehicle.GetHashCode() == i_LicenseNumber.GetHashCode();
            }

            return res;
        }

        public static bool operator !=(Vehicle i_Vehicle1, string i_LicenseNumber)
        {
            return !(i_Vehicle1 == i_LicenseNumber);
        }

        public void InfalteTiresToMax()
        {
            foreach (Tire tire in m_Tires)
            {
                tire.InflationToMax();
            }
        }

        public float EnergyLeft()
        {
            return m_Engine.CurrentAmount;
        }

        public override string ToString()
        {
            int currentIndexTire = 1;
            StringBuilder sbVehicle = new StringBuilder();

            sbVehicle.AppendLine();
            sbVehicle.AppendLine(this.GetType().Name);
            sbVehicle.AppendFormat("License Number: {0}", m_LicenseNumber);
            sbVehicle.AppendLine();
            sbVehicle.AppendFormat("Model Name: {0}", m_ModelName);
            sbVehicle.AppendLine();

            ExtendToString(sbVehicle);


            sbVehicle.Append("The vehicle runs on: ");
            sbVehicle.Append(m_Engine.ToString());

            foreach (Tire tire in m_Tires)
            {

                sbVehicle.AppendFormat("Tire #{0}", currentIndexTire);
                currentIndexTire++;
                sbVehicle.AppendLine(tire.ToString());
            }

            return sbVehicle.ToString();
        }

        public abstract void ExtendToString(StringBuilder io_Sb);

        public abstract List<string> RequestSpecificVehicleDetails();

        public abstract void ValidateAndSetSpecificVehicleDetails(List<string> i_UserInput);

    }
}
