using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingReservationSystem.Models
{
    public class ParkingSpotModel
    {
        public int Id { get; set; }
        public string ParkingSpotNumber { get; set; }
        public bool Available { get; set; }
        public ParkingSpotTypesEnum ParkingSpotType { get; set; }
        public bool Active { get; set; }
    }

    public class ParkingSpotHoldModel : ParkingSpotModel
    {
        public int Id { get; set; }
        public string PstId { get; set; }
        public int HeaderId { get; set; }
        public DateTime ParkedTime { get; set; }
        public DateTime? ReleasedTime { get; set; }
        public Decimal TotalAmount { get; set; }
    }
}
