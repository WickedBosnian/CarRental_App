using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
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
    public class VehicleManufacturerRepository : IVehicleManufacturerRepository
    {
        private CarRentalDBContext _context;
        public VehicleManufacturerRepository(CarRentalDBContext context)
        {
            _context = context;
        }

        public int CreateVehicleManufacturer(VehicleManufacturer vehicleManufacturer)
        {
            int createdVehicleManufacturerId = -1;
            string sqlCommand = "EXEC dbo.CreateVehicleManufacturer @VehicleManufacturerName, @VehicleManufacturerDescription";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleManufacturerName", Value = vehicleManufacturer.VehicleManufacturerName},
                    new SqlParameter { ParameterName = "@VehicleManufacturerDescription", Value = vehicleManufacturer.VehicleManufacturerDescription}
                };

                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Parameters.AddRange(sqlParams.ToArray());
                cmd.Connection.Open();

                createdVehicleManufacturerId = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return createdVehicleManufacturerId;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public int DeleteVehicleManufacturer(int id)
        {
            string sqlCommand = "EXEC dbo.DeleteVehicleManufacturer @VehicleManufacturerId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleManufacturerId", Value = id}
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

        public IEnumerable<VehicleManufacturer> GetAllVehicleManufacturers()
        {
            string sqlCommand = "EXEC dbo.GetAllVehicleManufacturers";

            try
            {
                return _context.VehicleManufacturers.FromSqlRaw(sqlCommand).AsEnumerable();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public VehicleManufacturer GetVehicleManufacturerById(int id)
        {
            string sqlCommand = "EXEC dbo.GetVehicleManufacturerById @VehicleManufacturerId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleManufacturerId", Value = id}
                };

                VehicleManufacturer? vehicleManufacturer = _context.VehicleManufacturers.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable().FirstOrDefault();
                if(vehicleManufacturer == null)
                {
                    throw new Exception($"Vehicle manufacturer with ID {id} was not found!");
                }

                return vehicleManufacturer;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public void UpdateVehicleManufacturer(VehicleManufacturer vehicleManufacturer)
        {
            string sqlCommand = "EXEC dbo.UpdateVehicleManufacturer @VehicleManufacturerId, @VehicleManufacturerName, @VehicleManufacturerDescription";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleManufacturerId", Value = vehicleManufacturer.VehicleManufacturerId},
                    new SqlParameter { ParameterName = "@VehicleManufacturerName", Value = String.IsNullOrEmpty(vehicleManufacturer.VehicleManufacturerName) ? DBNull.Value : vehicleManufacturer.VehicleManufacturerName},
                    new SqlParameter { ParameterName = "@VehicleManufacturerDescription", Value = String.IsNullOrEmpty(vehicleManufacturer.VehicleManufacturerDescription) ? DBNull.Value : vehicleManufacturer.VehicleManufacturerDescription},
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
    }
}
