using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
using CarRental_Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private CarRentalDBContext _context;

        public VehicleRepository(CarRentalDBContext context)
        {
            _context = context;
        }

        public int CreateVehicle(VehicleDTO vehicle)
        {
            int createdVehicleId = -1;
            string sqlCommand = "EXEC dbo.CreateVehicle @VehicleName, @VehicleManufacturerId, @VehicleTypeId, @Color, @DateManufactured, @PricePerDay";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleName", Value = vehicle.VehicleName},
                    new SqlParameter { ParameterName = "@VehicleManufacturerId", Value = vehicle.VehicleManufacturerId},
                    new SqlParameter { ParameterName = "@VehicleTypeId", Value = vehicle.VehicleTypeId},
                    new SqlParameter { ParameterName = "@Color", Value = String.IsNullOrEmpty(vehicle.Color) ? DBNull.Value : vehicle.Color},
                    new SqlParameter { ParameterName = "@DateManufactured", Value = vehicle.DateManufactured},
                    new SqlParameter { ParameterName = "@PricePerDay", Value = vehicle.PricePerDay}
                };

                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Parameters.AddRange(sqlParams.ToArray());
                cmd.Connection.Open();

                createdVehicleId = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return createdVehicleId;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public int DeleteVehicle(int id)
        {
            string sqlCommand = "EXEC dbo.DeleteVehicle @VehicleId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleId", Value = id}
                };

                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Parameters.AddRange(sqlParams.ToArray());
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

                return id;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public IEnumerable<VehicleDTO> GetAllVehicles()
        {
            string sqlCommand = "EXEC dbo.GetAllVehicles";

            try
            {
                IEnumerable<Vehicle> vehicles = _context.Vehicles.FromSqlRaw(sqlCommand).AsEnumerable();
                return vehicles.Select(x => Mapper.ToVehicleDTO(x));
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public VehicleDTO GetVehicleById(int id)
        {
            string sqlCommand = "EXEC dbo.GetVehicleById @VehicleId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleId", Value = id}
                };

                Vehicle? vehicle = _context.Vehicles.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable().FirstOrDefault();
                if (vehicle == null)
                {
                    throw new Exception($"Vehicle with ID {id} was not found!");
                }

                return Mapper.ToVehicleDTO(vehicle);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public IEnumerable<VehicleDTO> SearchVehicles(VehicleDTO vehicle, int pageNumber = 1, int rowsPerPage = 10)
        {
            string sqlCommand = "EXEC dbo.SearchVehicles @VehicleName, @VehicleManufacturerId, @VehicleTypeId, @PageNumber, @RowsPerPage";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleName", Value = String.IsNullOrEmpty(vehicle.VehicleName) ? DBNull.Value : vehicle.VehicleName},
                    new SqlParameter { ParameterName = "@VehicleManufacturerId", Value = vehicle.VehicleManufacturerId == null ? DBNull.Value : vehicle.VehicleManufacturerId},
                    new SqlParameter { ParameterName = "@VehicleTypeId", Value = vehicle.VehicleTypeId == null ? DBNull.Value : vehicle.VehicleTypeId},
                    new SqlParameter { ParameterName = "@PageNumber", Value = pageNumber},
                    new SqlParameter { ParameterName = "@RowsPerPage", Value = rowsPerPage}
                };

                IEnumerable<Vehicle> vehicles = _context.Vehicles.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable();
                return vehicles.Select(x => Mapper.ToVehicleDTO(x));
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public void UpdateVehicle(VehicleDTO vehicle)
        {
            string sqlCommand = "EXEC dbo.UpdateVehicle @VehicleId, @VehicleName, @VehicleManufacturerId, @VehicleTypeId, @Color, @DateManufactured, @PricePerDay";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleId", Value = vehicle.VehicleId},
                    new SqlParameter { ParameterName = "@VehicleName", Value = String.IsNullOrEmpty(vehicle.VehicleName) ? DBNull.Value : vehicle.VehicleName},
                    new SqlParameter { ParameterName = "@VehicleManufacturerId", Value = vehicle.VehicleManufacturerId == null ? DBNull.Value : vehicle.VehicleManufacturerId},
                    new SqlParameter { ParameterName = "@VehicleTypeId", Value = vehicle.VehicleTypeId == null ? DBNull.Value : vehicle.VehicleTypeId},
                    new SqlParameter { ParameterName = "@Color", Value = String.IsNullOrEmpty(vehicle.Color) ? "" : vehicle.Color},
                    new SqlParameter { ParameterName = "@DateManufactured", Value = vehicle.DateManufactured == null ? DBNull.Value : vehicle.DateManufactured},
                    new SqlParameter { ParameterName = "@PricePerDay", Value = vehicle.PricePerDay == null ? DBNull.Value : vehicle.PricePerDay}
                };

                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Parameters.AddRange(sqlParams.ToArray());

                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        /// <summary>
        /// This method calls a function from DB that returns count of records from table Client
        /// </summary>
        public int CountOfVehicles()
        {
            int countOfVehicles = -1;
            string sqlCommand = "SELECT dbo.CountOfVehicles()";

            try
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Connection.Open();

                countOfVehicles = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return countOfVehicles;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public int CountOfVehiclesWithFilters(VehicleDTO filterVehicle)
        {
            string sqlCommand = "SELECT dbo.CountOfVehiclesWithFilters(@VehicleName, @VehicleManufacturerId, @VehicleTypeId)";
            int countOfVehicles = -1;
            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleName", Value = String.IsNullOrEmpty(filterVehicle.VehicleName) ? DBNull.Value : filterVehicle.VehicleName},
                    new SqlParameter { ParameterName = "@VehicleManufacturerId", Value = filterVehicle.VehicleManufacturerId == null ? DBNull.Value : filterVehicle.VehicleManufacturerId},
                    new SqlParameter { ParameterName = "@VehicleTypeId", Value = filterVehicle.VehicleTypeId == null ? DBNull.Value : filterVehicle.VehicleTypeId}
                };

                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Parameters.AddRange(sqlParams.ToArray());
                cmd.Connection.Open();

                countOfVehicles = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return countOfVehicles;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public IEnumerable<VehicleDTO> GetVehiclesForPagination(int pageNumber, int rowsPerPage)
        {
            string sqlCommand = "EXEC dbo.GetVehiclesForPagination @PageNumber, @RowsPerPage";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@PageNumber", Value = pageNumber },
                    new SqlParameter { ParameterName = "@RowsPerPage", Value = rowsPerPage }
                };

                IEnumerable<Vehicle> vehicles = _context.Vehicles.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable();

                return vehicles.Select(x => Mapper.ToVehicleDTO(x));
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }
    }
}
