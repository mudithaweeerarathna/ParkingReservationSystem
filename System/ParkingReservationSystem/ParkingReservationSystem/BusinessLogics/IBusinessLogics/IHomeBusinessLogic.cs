using ParkingReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingReservationSystem.BusinessLogics.IBusinessLogics
{
    public interface IHomeBusinessLogic
    {
        #region Parking Spot

        void SaveParkingSpot(ParkingSpotModel parkingSpotModel);
        List<ParkingSpotModel> GetParkingSpots();
        string GetLastParkingSpotNumber();
        ParkingSpotModel GetParkingSpot(int id);
        void DeleteParkingSpot(int id);
        ParkingSpotHoldModel ReserveParkingSpot(ParkingSpotTypesEnum parkingSpotType);
        ParkingSpotHoldModel CheckOutCalculation(ParkingSpotHoldModel parkingSpotModel);

        #endregion

        #region Other Pages

        HomePageModel GetHomePageDetails();

        #endregion
    }
}
