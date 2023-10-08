using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ParkingReservationSystem.CommonFunctions;

namespace ParkingReservationSystem.DataAccess
{
    public class CommonDataAccess
    {
        private readonly ConvertionFunctions _convertionFunctions;
        private readonly IConfiguration configuration;
        string connectionString;

        public CommonDataAccess()
        {
            _convertionFunctions = new ConvertionFunctions();
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = configuration.GetValue<String>("ConnectionStrings:ParkingDatabase");
        }

        public int GetLastId(int instance)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("USP_GetLastId", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("Instance", SqlDbType.Int).Value = instance;
                    return _convertionFunctions.StringToIntConvert(sqlCommand.ExecuteScalar());
                }
            }
            catch (NullReferenceException ex)
            {
                throw;
            }
        }
    }
}
