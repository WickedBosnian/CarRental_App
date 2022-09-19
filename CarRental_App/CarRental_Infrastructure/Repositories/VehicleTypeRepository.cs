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
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        private CarRentalDBContext _context;

        public VehicleTypeRepository(CarRentalDBContext context)
        {
            _context = context;
        }
        public int CreateVehicleType(VehicleType vehicleType)
        {
            int createdVehicleType = -1;
            string sqlCommand = "EXEC dbo.CreateVehicleType @VehicleTypeName, @VehicleTypeDescription";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleTypeName", Value = vehicleType.VehicleTypeName},
                    new SqlParameter { ParameterName = "@VehicleTypeDescription", Value = String.IsNullOrEmpty(vehicleType.VehicleTypeDescription) ? DBNull.Value : vehicleType.VehicleTypeDescription}
                };

                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Parameters.AddRange(sqlParams.ToArray());
                cmd.Connection.Open();

                createdVehicleType = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return createdVehicleType;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public int DeleteVehicleType(int id)
        {
            string sqlCommand = "EXEC dbo.DeleteVehicleType @VehicleTypeId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleTypeId", Value = id}
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

        public IEnumerable<VehicleType> GetAllVehicleTypes()
        {
            string sqlCommand = "EXEC dbo.GetAllVehicleTypes";

            try
            {
                return _context.VehicleTypes.FromSqlRaw(sqlCommand).AsEnumerable();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public VehicleType GetVehicleTypeById(int id)
        {
            string sqlCommand = "EXEC dbo.GetVehicleTypeById @VehicleTypeId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleTypeId", Value = id}
                };

                VehicleType? vehicleType = _context.VehicleTypes.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable().FirstOrDefault();

                if(vehicleType == null)
                {
                    throw new Exception($"Vehicle type with ID {id} was not found!");
                }

                return vehicleType;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public void UpdateVehicleType(VehicleType vehicleType)
        {
            string sqlCommand = "EXEC dbo.UpdateVehicleType @VehicleTypeId, @VehicleTypeName, @VehicleTypeDescription";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicleTypeId", Value = vehicleType.VehicleTypeId},
                    new SqlParameter { ParameterName = "@VehicleTypeName", Value = String.IsNullOrEmpty(vehicleType.VehicleTypeName) ? DBNull.Value : vehicleType.VehicleTypeName},
                    new SqlParameter { ParameterName = "@VehicleTypeDescription", Value = String.IsNullOrEmpty(vehicleType.VehicleTypeDescription) ? DBNull.Value : vehicleType.VehicleTypeDescription}
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
