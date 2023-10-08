using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingReservationSystem.DataAccess
{
    public class DataAccess
    {
        //String connectionString;
        IConfiguration configuration;
        
        //public SqlConnection CreateConnection()
        //{

        //    //configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //    //var connectionString = configuration.GetValue<String>("ConnectionStrings:ParkingDatabase");

        //    //SqlConnection sqlConnection;
        //    //sqlConnection = new SqlConnection(connectionString);
        //    //return sqlConnection; //configuration.GetConnectionString("ParkingDatabase").ToString();
        //}
    }
}
