using System;
using System.Text;

namespace Ex03.GrarageLogic
{
    public class Tire
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public const int k_MinAirPressure = 0;

        public Tire(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
            set
            {
                m_ManufacturerName = value;

            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value < k_MinAirPressure || value > r_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(k_MinAirPressure, r_MaxAirPressure);
                }
                else
                {
                    m_CurrentAirPressure = value;
                }
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public bool Inflation(float i_AmountOfAir)
        {
            bool canAdd;
            if (i_AmountOfAir < k_MinAirPressure || i_AmountOfAir + m_CurrentAirPressure <= r_MaxAirPressure)
            {
                canAdd = false;
            }
            else
            {
                m_CurrentAirPressure += i_AmountOfAir;
                canAdd = true;
            }

            return canAdd;
        }

        public void InflationToMax()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        public override string ToString()
        {
            StringBuilder sbTire = new StringBuilder();
            sbTire.AppendLine();
            sbTire.AppendFormat("ManufacturerName : {0}", m_ManufacturerName);
            sbTire.AppendLine();
            sbTire.AppendFormat("Current Air Pressure: {0}", m_CurrentAirPressure);
            sbTire.AppendLine();

            return sbTire.ToString();
        }
    }
}
