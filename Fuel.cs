using System;
using System.Text;

namespace Ex03.GrarageLogic
{
    public class Fuel : EnergyResource
    {
        public enum eFuelType
        {
            Octan98 = 1,
            Octan96,
            Octan95,
            Soler,
        }

        public const int k_NumOfFuelType = 4;
        protected readonly eFuelType r_FuelType;
        protected const string k_FuelUnits = "liters";

        public Fuel(float i_MaxAmount, eFuelType i_FuelType) : base(i_MaxAmount)
        {
            r_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public override void AddEnergy(float i_AmountToAdd, eFuelType? i_FuelType)
        {
            if (i_FuelType == null)
            {
                throw new ArgumentException(string.Format("Can not charge battery of a fuel vehicle."));
            }
            else if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException(string.Format("Invalid fuel type, Please insert valid fuel type: {0}", r_FuelType));
            }
            else if (i_AmountToAdd < k_MinAmout || i_AmountToAdd + m_CurrentAmount > r_MaxAmount)
            {
                float topRange = r_MaxAmount - m_CurrentAmount;
                throw new ValueOutOfRangeException(k_MinAmout, topRange);
            }
            else
            {
                m_CurrentAmount += i_AmountToAdd;
            }
        }

        public override void ExtendToString(StringBuilder io_Sb)
        {
            io_Sb.AppendFormat("Fuel type: {0}", r_FuelType);
            io_Sb.AppendLine();
            io_Sb.AppendFormat("The current amount of fuel in the tank is: {0} {1}.", m_CurrentAmount, k_FuelUnits);
            io_Sb.AppendLine();
        }

    }
}
