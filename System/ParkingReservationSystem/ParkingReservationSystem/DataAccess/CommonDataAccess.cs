using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ParkingReservationSystem.CommonFunctions;

namespace ParkingReservationSystem.DataAccess
{
    public class CommonDataAccess
    {
        private readonly SqlConnection connection;
        private readonly ConvertionFunctions _convertionFunctions;

        public CommonDataAccess()
        {
            _convertionFunctions = new ConvertionFunctions();
            DataAccess dataAccess = new DataAccess();
            connection = dataAccess.CreateConnection();
            connection.Open();
        }

        public int GetLastId(int instance)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("USP_GetLastId", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("Instance", SqlDbType.Int).Value = instance;
                return _convertionFunctions.StringToIntConvert(sqlCommand.ExecuteScalar());
            }
            catch (NullReferenceException ex)
            {
                throw;
            }
        }
    }
}
