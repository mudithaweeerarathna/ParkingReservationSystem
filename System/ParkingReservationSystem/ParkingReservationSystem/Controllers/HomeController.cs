﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingReservationSystem.BusinessLogics;
using ParkingReservationSystem.BusinessLogics.IBusinessLogics;
using ParkingReservationSystem.Models;

namespace ParkingReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeBusinessLogic _homeBusinessLogic;

        public HomeController(IHomeBusinessLogic homeBusinessLogic)
        {
            _homeBusinessLogic = homeBusinessLogic;
        }

        #region Parking Spot

        [HttpGet]
        public ActionResult CreateParkingSpot()
        {
            ViewBag.spotNumber = _homeBusinessLogic.GetLastParkingSpotNumber();
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateParkingSpot(ParkingSpotModel parkingSpotModel)
        {
            _homeBusinessLogic.SaveParkingSpot(parkingSpotModel);
            return RedirectToAction("ParkingSpotList");
        }
        
        [HttpGet]
        public ActionResult EditParkingSpot(int id)
        {
            ParkingSpotModel parkingSpotModel = new ParkingSpotModel();
            parkingSpotModel = _homeBusinessLogic.GetParkingSpot(id);
            return View("CreateParkingSpot", parkingSpotModel);
        }
        
        [HttpGet]
        public IActionResult ParkingSpotList()
        {
            List<ParkingSpotModel> parkingSpotModel = new List<ParkingSpotModel>();
            parkingSpotModel = _homeBusinessLogic.GetParkingSpots();
            return View(parkingSpotModel);
        }

        [HttpGet]
        public ActionResult DeleteParkingSpot(int id)
        {
            _homeBusinessLogic.DeleteParkingSpot(id);
            return RedirectToAction("ParkingSpotList");
        }

        #endregion

        #region Parking Reservation and check out

        public ViewResult GetParkingReservation()
        {
            return View();
        }

        public ViewResult ReserveParkingSpot(ParkingSpotTypesEnum type) //add the correct functions in the controller
        {
            var parkingSpotHold = new ParkingSpotHoldModel();
            parkingSpotHold = _homeBusinessLogic.ReserveParkingSpot(type);
            return View(parkingSpotHold);
        }

        public ViewResult CheckOut()
        {
            return View();
        }

        public ViewResult CheckOutCalculation(ParkingSpotHoldModel parkingSpotModel)
        {
            var ParkingSpotHoldModel = new ParkingSpotHoldModel();
            ParkingSpotHoldModel = _homeBusinessLogic.CheckOutCalculation(parkingSpotModel);
            return View(ParkingSpotHoldModel);
        }

        #endregion

    }
}
