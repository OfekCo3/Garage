using System;
using System.Text;

namespace Ex03.GrarageLogic
{
    public class VehicleCard
    {
        public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired,
            Paid,
        }

        public const int k_VehicleStatusSize = 3;
        private string m_OwnerName;
        private string m_OwnerNumber;
        private eVehicleStatus m_VehicleStatus = eVehicleStatus.InRepair;
        private Vehicle m_Vehicle;

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerNumber
        {
            get
            {
                return m_OwnerNumber;
            }
            set
            {
                m_OwnerNumber = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }
            set
            {
                ChangeStatus(value);
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
            set
            {
                m_Vehicle = value;
            }
        }

        public void ChangeStatus(eVehicleStatus i_Status)
        {
            switch (i_Status)
            {
                case eVehicleStatus.InRepair:
                    m_VehicleStatus = i_Status;
                    break;
                case eVehicleStatus.Paid:
                    PayingForTheRepair();
                    break;
                case eVehicleStatus.Repaired:
                    Repaired();
                    break;
            }
        }

        public void PayingForTheRepair()
        {
            if (IsPaidForTheRepair())
            {
                throw new ArgumentException("Already paid for this repair!");
            }
            else if (IsInRepair())
            {
                throw new ArgumentException(String.Format("The {0} is still under repair. "
                    , m_Vehicle.GetType().Name));
            }
            else
            {
                m_VehicleStatus = eVehicleStatus.Paid;
            }
        }

        public bool IsPaidForTheRepair()
        {
            return (m_VehicleStatus == eVehicleStatus.Paid);
        }

        public bool IsInRepair()
        {
            return (m_VehicleStatus == eVehicleStatus.InRepair);
        }

        public bool IsRepaired()
        {
            return (m_VehicleStatus != eVehicleStatus.Repaired);
        }

        public void Repaired()
        {
            if (IsPaidForTheRepair() || IsRepaired())
            {
                throw new ArgumentException(string.Format("The {0} has already been repaired", m_Vehicle.GetType().Name));
            }
            else
            {
                m_VehicleStatus = eVehicleStatus.Repaired;
            }
        }

        public override string ToString()
        {
            StringBuilder sbVehicleCard = new StringBuilder();
            sbVehicleCard.AppendFormat("Owner Name: {0}", m_OwnerName);
            sbVehicleCard.AppendLine();
            sbVehicleCard.AppendFormat("Owner Number: {0}", m_OwnerNumber);
            sbVehicleCard.AppendLine();
            sbVehicleCard.AppendFormat("Vehicle Status: {0}", m_VehicleStatus);
            sbVehicleCard.AppendLine();
            sbVehicleCard.AppendLine(m_Vehicle.ToString());

            return sbVehicleCard.ToString();
        }
    }
}
