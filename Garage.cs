using System;
using System.Collections.Generic;
using static Ex03.GrarageLogic.VehicleCard;
using static Ex03.GrarageLogic.Fuel;

namespace Ex03.GrarageLogic
{
    public class Garage
    {
        protected Dictionary<string, VehicleCard> m_VehicleCards = new Dictionary<string, VehicleCard>();

        public void AddNewVehicleCard(string i_LicenseNumber, VehicleCard i_VehicleCard)
        {
            m_VehicleCards.Add(i_LicenseNumber, i_VehicleCard);
        }

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return m_VehicleCards.ContainsKey(i_LicenseNumber);
        }

        public VehicleCard GetVehicleCardFromGarage(string i_LicenseNumber)
        {
            return m_VehicleCards[i_LicenseNumber];
        }

        public List<string> GetVehiclesLicenseNumbersSortByStatus(eVehicleStatus? i_VehicleStatus)
        {
            List<string> res = new List<string>();

            foreach (KeyValuePair<string, VehicleCard> vehicleCard in m_VehicleCards)
            {
                if (i_VehicleStatus != null)
                {
                    if (vehicleCard.Value.VehicleStatus == i_VehicleStatus)
                    {
                        res.Add(vehicleCard.Key);
                    }
                }
                else
                {
                    res.Add(vehicleCard.Key);
                }
            }

            return res;
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_VehicleStatus)
        {
            m_VehicleCards[i_LicenseNumber].VehicleStatus = i_VehicleStatus;
        }

        public void InflateTiresToMax(string i_LicenseNumber)
        {
            m_VehicleCards[i_LicenseNumber].Vehicle.InfalteTiresToMax();
        }

        public void AddEnergy(string i_LicenseNumber, float i_AmountToAdd, eFuelType? i_FuelType)
        {
            m_VehicleCards[i_LicenseNumber].Vehicle.Engine.AddEnergy(i_AmountToAdd, i_FuelType);
        }

        public string GetSpecificVehicleData(string i_LicenseNumber)
        {
            return m_VehicleCards[i_LicenseNumber].Vehicle.ToString();
        }

        public bool IsGarageEmpty()
        {
            return m_VehicleCards.Count == 0;
        }

    }
}
