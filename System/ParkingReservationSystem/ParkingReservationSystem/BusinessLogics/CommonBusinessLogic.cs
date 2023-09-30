using ParkingReservationSystem.DataAccess;
using ParkingReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingReservationSystem.BusinessLogics
{
    public class CommonBusinessLogic
    {
        CommonDataAccess _commonDataAccess;

        public CommonBusinessLogic()
        {
            _commonDataAccess = new CommonDataAccess();   
        }

        public int GetLastId(int instance)
        {
            return _commonDataAccess.GetLastId(instance);
        }

        public string GetLastParkingSpotHoldId()
        {
            try
            {
                int Id = GetLastId(1);
                Id = Id + 1;
                string lastId = Id < 10 && Id > 0 ? ("PSTID-00" + Id) : ("PSTID-" + Id);
                return lastId;
            }
            catch(Exception ex)
            {
                throw; 
            }
        }

        public double CalculateNoOfHours(DateTime parkedTime)
        {
            DateTime currentTime = DateTime.Now;
            double totalHours = currentTime.Subtract(parkedTime).TotalHours;
            return totalHours;
        }

        public double CalculateParkingFees(DateTime parkedTime, ParkingSpotTypesEnum parkingSpotType)
        {
            double noOfHours = CalculateNoOfHours(parkedTime);
            double totalParkingFee = 0.00;

            if(parkingSpotType == ParkingSpotTypesEnum.Large)
            {
                totalParkingFee = (double) ParkingSpotPricesEnum.Large * noOfHours;
            }
            else if(parkingSpotType == ParkingSpotTypesEnum.Medium)
            {
                totalParkingFee = (double)ParkingSpotPricesEnum.Medium * noOfHours;
            }
            else if (parkingSpotType == ParkingSpotTypesEnum.Small)
            {
                totalParkingFee = (double)ParkingSpotPricesEnum.Small * noOfHours;
            }

            return totalParkingFee;
        }
    }
}
