using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingReservationSystem.Models
{
    public class HomePageModel
    {
        public int TotalParkingSpots { get; set; }
        public int AvailablePS { get; set; }
        public int OccupiedPS { get; set; }
        public int AvailableLargeTypePS { get; set; }
        public int OccupiedLargeTypePS { get; set; }
        public int AvailableMediumTypePS { get; set; }
        public int OccupiedMediumTypePS { get; set; }
        public int AvailableSmallTypePS { get; set; }
        public int OccupiedSmallTypePS { get; set; }
    }
}
