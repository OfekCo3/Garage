using System;
using System.Text;
using static Ex03.GrarageLogic.Fuel;

namespace Ex03.GrarageLogic
{
    public class Electric : EnergyResource
    {
        protected const string k_BatteryUnits = "kilowatt-hours";

        public Electric(float i_MaxAmount) : base(i_MaxAmount) { }

        public override void AddEnergy(float i_AmountToAdd, eFuelType? i_FuelType)
        {
            if (i_FuelType != null)
            {
                throw new ArgumentException(string.Format("Can not add fuel to electirc vehicle."));
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
            io_Sb.AppendFormat("The current battery capacity is: {0} {1}.", m_CurrentAmount, k_BatteryUnits);
            io_Sb.AppendLine();
        }

    }
}
