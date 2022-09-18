﻿using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private CarRentalDBContext _context;

        public ClientRepository(CarRentalDBContext context)
        {
            _context = context;
        }

        public int CreateClient(Client client)
        {
            int createdClientId = -1;
            string sqlCommand = "EXEC dbo.CreateClient @Firstname, @Lastname, @DriverLicenceNumber, @PersonalIdcardNumber, @Birthdate, @Gender";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@Firstname", Value = client.Firstname},
                    new SqlParameter { ParameterName = "@Lastname", Value = client.Lastname},
                    new SqlParameter { ParameterName = "@DriverLicenceNumber", Value = client.DriverLicenceNumber},
                    new SqlParameter { ParameterName = "@PersonalIdcardNumber", Value = client.PersonalIdcardNumber},
                    new SqlParameter { ParameterName = "@Birthdate", Value = client.Birthdate},
                    new SqlParameter { ParameterName = "@Gender", Value = client.Gender}
                };

                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Parameters.AddRange(sqlParams.ToArray());
                cmd.Connection.Open();

                createdClientId = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return createdClientId;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public int DeleteClient(int id)
        {
            string sqlCommand = "EXEC dbo.DeleteClient @ClientId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@ClientId", Value = id}
                };

                _context.Clients.FromSqlRaw(sqlCommand, sqlParams.ToArray());

                return id;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public IEnumerable<Client> GetAllClients()
        {
            string sqlCommand = "EXEC dbo.GetAllClients";

            try
            {
                return _context.Clients.FromSqlRaw(sqlCommand).AsEnumerable();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public Client GetClientById(int id)
        {
            string sqlCommand = "EXEC dbo.GetClientById @ClientId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@ClientId", Value = id}
                };

                return _context.Clients.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable().First();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public IEnumerable<Client> GetClientsByFilters(DateTime? birthdate, string? firstname, string? lastname, string? driverLicenceNumber, string? personalIdCardNumber, string? gender)
        {
            string sqlCommand = "EXEC dbo.GetClientsByFilters @Firstname, @Lastname, @DriverLicenceNumber, @PersonalIDCardNumber, @Birthdate, @Gender";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@Firstname", Value = String.IsNullOrEmpty(firstname) ? DBNull.Value : firstname},
                    new SqlParameter { ParameterName = "@Lastname", Value = String.IsNullOrEmpty(lastname) ? DBNull.Value : lastname},
                    new SqlParameter { ParameterName = "@DriverLicenceNumber", Value = String.IsNullOrEmpty(driverLicenceNumber) ? DBNull.Value : driverLicenceNumber},
                    new SqlParameter { ParameterName = "@PersonalIdcardNumber", Value = String.IsNullOrEmpty(personalIdCardNumber) ? DBNull.Value : personalIdCardNumber},
                    new SqlParameter { ParameterName = "@Birthdate", Value = birthdate == null ? DBNull.Value : birthdate},
                    new SqlParameter { ParameterName = "@Gender", Value = String.IsNullOrEmpty(gender) ? DBNull.Value : gender}
                };

                return _context.Clients.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public void UpdateClient(Client client)
        {
            string sqlCommand = "EXEC dbo.UpdateClient @ClientId, @Firstname, @Lastname, @DriverLicenceNumber, @PersonalIdcardNumber, @Birthdate, @Gender";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@ClientId", Value = client.ClientId},
                    new SqlParameter { ParameterName = "@Firstname", Value = String.IsNullOrEmpty(client.Firstname) ? DBNull.Value : client.Firstname},
                    new SqlParameter { ParameterName = "@Lastname", Value = String.IsNullOrEmpty(client.Lastname) ? DBNull.Value : client.Lastname},
                    new SqlParameter { ParameterName = "@DriverLicenceNumber", Value = String.IsNullOrEmpty(client.DriverLicenceNumber) ? DBNull.Value : client.DriverLicenceNumber},
                    new SqlParameter { ParameterName = "@PersonalIdcardNumber", Value = String.IsNullOrEmpty(client.PersonalIdcardNumber) ? DBNull.Value : client.PersonalIdcardNumber},
                    new SqlParameter { ParameterName = "@Birthdate", Value = client.Birthdate == null ? DBNull.Value : client.Birthdate},
                    new SqlParameter { ParameterName = "@Gender", Value = String.IsNullOrEmpty(client.Gender) ? DBNull.Value : client.Gender}
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