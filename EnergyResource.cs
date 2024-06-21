using System.Text;
using static Ex03.GrarageLogic.Car;
using static Ex03.GrarageLogic.Fuel;

namespace Ex03.GrarageLogic
{
    public abstract class EnergyResource
    {
        protected float m_CurrentAmount;
        protected readonly float r_MaxAmount;
        protected const float k_MinAmout = 0f;

        protected EnergyResource(float i_MaxAmount)
        {
            r_MaxAmount = i_MaxAmount;
        }

        public float CurrentAmount
        {
            get
            {
                return m_CurrentAmount;
            }
            set
            {
                if (value < k_MinAmout || value > r_MaxAmount)
                {
                    throw new ValueOutOfRangeException(k_MinAmout, r_MaxAmount);
                }
                else
                {
                    m_CurrentAmount = value;
                }
            }
        }

        public float MaxAmount
        {
            get
            {
                return r_MaxAmount;
            }
        }

        public override string ToString()
        {
            StringBuilder sb_Vehicle = new StringBuilder();
            sb_Vehicle.Append(this.GetType().Name);
            sb_Vehicle.AppendLine();
            ExtendToString(sb_Vehicle);
            
            return sb_Vehicle.ToString();
        }

        public abstract void AddEnergy(float i_AmountToAdd, eFuelType? i_FuelType);

        public abstract void ExtendToString(StringBuilder sb);

    }
}
