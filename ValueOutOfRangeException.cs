using System;

namespace Ex03.GrarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        protected float m_MaxValue;
        protected float m_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format("Value must be in range ({0},{1})", i_MinValue, i_MaxValue))
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

        float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }

        float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }

    }
}

