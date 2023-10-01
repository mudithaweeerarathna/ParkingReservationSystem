using ParkingReservationSystem.BusinessLogics.IBusinessLogics;
using ParkingReservationSystem.DataAccess;
using ParkingReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingReservationSystem.BusinessLogics
{
    public class HomeBusinessLogic : IHomeBusinessLogic
    {
        HomeDataAccess _homeDataAccess;
        CommonBusinessLogic _commonBusinessLogic;

        public HomeBusinessLogic()
        {
            _homeDataAccess = new HomeDataAccess();
            _commonBusinessLogic = new CommonBusinessLogic();
        }

        #region Save Get Parking Spot Details and Calculations

        public void SaveParkingSpot(ParkingSpotModel parkingSpotModel)
        {
            try
            {
                parkingSpotModel.Active = true;
                if (parkingSpotModel.Id > 0)
                {
                    _homeDataAccess.SaveParkingSpot(2, parkingSpotModel);
                }
                else
                {
                    parkingSpotModel.Available = true;
                    _homeDataAccess.SaveParkingSpot(1, parkingSpotModel);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<ParkingSpotModel> GetParkingSpots()
        {
            return _homeDataAccess.GetParkingSpots(1, -1, -1);
        }

        public string GetLastParkingSpotNumber()
        {
            string lastParkingSpotNumber;
            var getParkingSpot = GetLastParkingSpot();
            int id = getParkingSpot == null ? 0 : getParkingSpot.Id;
            lastParkingSpotNumber = _commonBusinessLogic.GetLastParkingSpotId(id);
            return lastParkingSpotNumber;
        }

        public ParkingSpotModel GetLastParkingSpot()
        {
            return _homeDataAccess.GetParkingSpots(3, -1, -1)?.FirstOrDefault();
        }

        public ParkingSpotModel GetParkingSpot(int id)
        {
            return _homeDataAccess.GetParkingSpots(1, id, -1)?.FirstOrDefault();
        }

        public List<ParkingSpotModel> GetAllAvailableParkingSpots()
        {
            return _homeDataAccess.GetParkingSpots(2, -1, -1);
        }

        public List<ParkingSpotModel> GetAllAvailableParkingSpotsBySpotId(ParkingSpotTypesEnum parkingSpotType)
        {
            return _homeDataAccess.GetParkingSpots(2, -1, (int)parkingSpotType);
        }

        public void DeleteParkingSpot(int id)
        {
            _homeDataAccess.DeleteParkingSpot(4, id);
        }

        public ParkingSpotHoldModel ReserveParkingSpot(ParkingSpotTypesEnum parkingSpotType)
        {
            try
            {
                var parkingSpotHold = new ParkingSpotHoldModel();
                SetReserveParkingSpotDetais(parkingSpotType, parkingSpotHold);
                _homeDataAccess.ReserveParkingSpot(1, parkingSpotHold);
                return parkingSpotHold;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void SetReserveParkingSpotDetais(ParkingSpotTypesEnum parkingSpotType, ParkingSpotHoldModel parkingSpotHold)
        {
            try
            {
                ParkingSpotModel parkingSpot = GetAllAvailableParkingSpotsBySpotId(parkingSpotType)?.FirstOrDefault();
                parkingSpotHold.HeaderId = parkingSpot.Id;
                parkingSpotHold.ParkedTime = DateTime.Now;
                parkingSpotHold.ParkingSpotNumber = parkingSpot.ParkingSpotNumber;
                parkingSpotHold.PstId = _commonBusinessLogic.GetLastParkingSpotHoldId();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<ParkingSpotHoldModel> GetParkingSpotHoldDetailsById(string psId)
        {
            return _homeDataAccess.GetParkingSpotHoldDetails(1, psId);
        }

        public ParkingSpotHoldModel CheckOutCalculation(ParkingSpotHoldModel parkingSpotHoldModel)
        {
            try
            {
                ParkingSpotHoldModel parkingSpotHold = new ParkingSpotHoldModel();
                parkingSpotHold = GetParkingSpotHoldDetailsById(parkingSpotHoldModel.PstId).FirstOrDefault();
                var parkingSpotType = GetParkingSpot(parkingSpotHold.HeaderId).ParkingSpotType;
                parkingSpotHold.ReleasedTime = DateTime.Now;
                parkingSpotHold.TotalAmount = (decimal) _commonBusinessLogic.CalculateParkingFees(parkingSpotHold.ParkedTime, parkingSpotType);
                _homeDataAccess.ReserveParkingSpot(2, parkingSpotHold);
                return parkingSpotHold;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
