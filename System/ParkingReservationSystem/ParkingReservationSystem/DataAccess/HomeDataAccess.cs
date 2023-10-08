using ParkingReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using ParkingReservationSystem.CommonFunctions;

namespace ParkingReservationSystem.DataAccess
{
    public class HomeDataAccess
    {
        private readonly ConvertionFunctions _convertionFunctions;
        private readonly IConfiguration configuration;
        string connectionString;

        public HomeDataAccess()
        {
            _convertionFunctions = new ConvertionFunctions();
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = configuration.GetValue<String>("ConnectionStrings:ParkingDatabase");
        }

        #region Parking Spot

        //Save Parking Spot
        public void SaveParkingSpot(int instance, ParkingSpotModel parkingSpotModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("USP_SaveParkingSpot", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Instance", SqlDbType.Int).Value = instance;
                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = parkingSpotModel.Id;
                sqlCommand.Parameters.Add("@ParkingSpotNumber", SqlDbType.VarChar).Value = parkingSpotModel.ParkingSpotNumber;
                sqlCommand.Parameters.Add("@ParkingSpotType", SqlDbType.Int).Value = parkingSpotModel.ParkingSpotType;
                sqlCommand.Parameters.Add("@Available", SqlDbType.Bit).Value = parkingSpotModel.Available;
                sqlCommand.Parameters.Add("@Active", SqlDbType.Bit).Value = parkingSpotModel.Active;
                sqlCommand.ExecuteNonQuery();
            }
        }

        //Get Parking Spots
        public List<ParkingSpotModel> GetParkingSpots(int instance, int id, int parkingSpotType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("USP_GetParkingSpots", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Instance", SqlDbType.Int).Value = instance;
                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.Add("@ParkingSpotType", SqlDbType.Int).Value = parkingSpotType;
                return GetParkingSpots(sqlCommand.ExecuteReader());
            }
        }
        public List<ParkingSpotModel> GetParkingSpots(SqlDataReader sqlDataReader)
        {
            List<ParkingSpotModel> list = null;
            try
            {
                if (sqlDataReader.HasRows)
                {
                    list = new List<ParkingSpotModel>();
                    while (sqlDataReader.Read())
                    {
                        list.Add(
                        new ParkingSpotModel
                        {
                            Id = Convert.ToInt32(sqlDataReader["Id"].ToString()),
                            ParkingSpotNumber = sqlDataReader["ParkingSpotNumber"].ToString(),
                            ParkingSpotType = (ParkingSpotTypesEnum)Convert.ToInt32(sqlDataReader["ParkingSpotType"].ToString()),
                            Available = Convert.ToBoolean(sqlDataReader["Available"].ToString()),
                            Active = Convert.ToBoolean(sqlDataReader["Active"].ToString())
                        });
                    }
                }
                return list;
            }
            finally
            {
                sqlDataReader.Close();
            }
        }

        //Delete Parking Spot
        public void DeleteParkingSpot(int instance, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("USP_SaveParkingSpot", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Instance", SqlDbType.Int).Value = instance;
                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                sqlCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region Parking Spot Hold

        //Reserve Parking Spot
        public void ReserveParkingSpot(int instance, ParkingSpotHoldModel parkingSpotHoldModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("USP_ReserveParkingSpot", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("Id", SqlDbType.Int).Value = parkingSpotHoldModel.Id;
                sqlCommand.Parameters.Add("Instance", SqlDbType.Int).Value = instance;
                sqlCommand.Parameters.Add("HeaderId", SqlDbType.Int).Value = parkingSpotHoldModel.HeaderId;
                sqlCommand.Parameters.Add("PstId", SqlDbType.VarChar).Value = parkingSpotHoldModel.PstId;
                sqlCommand.Parameters.Add("ParkedTime", SqlDbType.DateTime).Value = parkingSpotHoldModel.ParkedTime;
                sqlCommand.Parameters.Add("ReleasedTime", SqlDbType.DateTime).Value = parkingSpotHoldModel.ReleasedTime;
                sqlCommand.Parameters.Add("TotalAmount", SqlDbType.Decimal).Value = parkingSpotHoldModel.TotalAmount;
                sqlCommand.ExecuteNonQuery();
            }
        }

        //Get Parking Spot Hold Details
        public List<ParkingSpotHoldModel> GetParkingSpotHoldDetails(int instance, string pstId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("USP_GetParkingSpotHoldDetails", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("Instance", SqlDbType.Int).Value = instance;
                sqlCommand.Parameters.Add("PstId", SqlDbType.VarChar).Value = pstId;
                return GetParkingSpotHoldDetails(sqlCommand.ExecuteReader());
            }
        }

        public List<ParkingSpotHoldModel> GetParkingSpotHoldDetails(SqlDataReader sqlDataReader)
        {
            List<ParkingSpotHoldModel> list= null;
            try
            {
                if (sqlDataReader.HasRows)
                {
                    list = new List<ParkingSpotHoldModel>();
                    while (sqlDataReader.Read())
                    {
                        list.Add(new ParkingSpotHoldModel
                        {
                            Id = Convert.ToInt32(sqlDataReader["Id"].ToString()),
                            PstId = sqlDataReader["PstId"].ToString(),
                            HeaderId = Convert.ToInt32(sqlDataReader["HeaderId"].ToString()),
                            ParkedTime = Convert.ToDateTime(sqlDataReader["ParkedTime"].ToString() ?? null),
                            ReleasedTime = _convertionFunctions.DateTimeConvertion(sqlDataReader["ReleasedTime"].ToString()),
                            TotalAmount = Convert.ToDecimal(sqlDataReader["TotalAmount"].ToString())
                        });
                    }
                }
                return list;
            }
            finally
            {
                sqlDataReader.Close();
            }
        }

        #endregion

        #region Other Pages

        public HomePageModel GetHomePageDetails(int instance)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("USP_GetAllParkingSpotTypeCount", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("Instance", SqlDbType.Int).Value = instance;
                return GetHomePageDetails(sqlCommand.ExecuteReader());
            }
        }

        public HomePageModel GetHomePageDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                HomePageModel homePageModel = null;
                if (sqlDataReader.HasRows)
                {
                    while(sqlDataReader.Read())
                    {
                        homePageModel = new HomePageModel()
                        {
                            TotalParkingSpots = _convertionFunctions.StringToIntConvert(sqlDataReader["TotalParkingSpots"].ToString()),
                            AvailablePS = _convertionFunctions.StringToIntConvert(sqlDataReader["AvailablePS"].ToString()),
                            OccupiedPS = _convertionFunctions.StringToIntConvert(sqlDataReader["OccupiedPS"].ToString()),
                            AvailableLargeTypePS = _convertionFunctions.StringToIntConvert(sqlDataReader["AvailableLargeTypePS"].ToString()),
                            OccupiedLargeTypePS = _convertionFunctions.StringToIntConvert(sqlDataReader["OccupiedLargeTypePS"].ToString()),
                            AvailableMediumTypePS = _convertionFunctions.StringToIntConvert(sqlDataReader["AvailableMediumTypePS"].ToString()),
                            OccupiedMediumTypePS = _convertionFunctions.StringToIntConvert(sqlDataReader["OccupiedMediumTypePS"].ToString()),
                            AvailableSmallTypePS = _convertionFunctions.StringToIntConvert(sqlDataReader["AvailableSmallTypePS"].ToString()),
                            OccupiedSmallTypePS = _convertionFunctions.StringToIntConvert(sqlDataReader["OccupiedSmallTypePS"].ToString()),
                        };
                    }
                }
                return homePageModel;
            }
            finally
            {
                sqlDataReader.Close();
            }
        }

        #endregion
    }
}
