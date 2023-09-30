using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingReservationSystem.DataAccess
{
    public class CommonDataAccess
    {
        private readonly SqlConnection connection;

        public CommonDataAccess()
        {
            DataAccess dataAccess = new DataAccess();
            connection = dataAccess.CreateConnection();
            connection.Open();
        }

        public int GetLastId(int instance)
        {
            SqlCommand sqlCommand = new SqlCommand("USP_GetLastId", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("Instance", SqlDbType.Int).Value = instance;
            return (int) sqlCommand.ExecuteScalar();
        }
    }
}
