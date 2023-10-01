using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingReservationSystem.CommonFunctions
{
    public class ConvertionFunctions
    {
        public DateTime DateTimeConvertion(string dateTime)
        {
            DateTime result;
            DateTime.TryParse(dateTime, out result);
            return result;
        }

        public int StringToIntConvert(object value)
        {
            int convertedValue = 0;
            if (value != null) convertedValue = Convert.ToInt32(value);
            return convertedValue;
        }
    }
}

