using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
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

        /// <summary>
        /// This method calls a function from DB that returns count of records from table Client
        /// </summary>
        public int CountOfClients()
        {
            int countOfClients = -1;
            string sqlCommand = "SELECT dbo.CountOfClients()";

            try
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Connection.Open();

                countOfClients = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return countOfClients;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public int CountOfClientsWithFilters(ClientDTO filterClient)
        {
            string sqlCommand = "SELECT dbo.CountOfClientsWithFilters(@Firstname, @Lastname, @DriverLicenceNumber, @PersonalIDCardNumber, @Birthdate, @Gender)";
            int countOfClients = -1;
            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@Firstname", Value = String.IsNullOrEmpty(filterClient.Firstname) ? DBNull.Value : filterClient.Firstname},
                    new SqlParameter { ParameterName = "@Lastname", Value = String.IsNullOrEmpty(filterClient.Lastname) ? DBNull.Value : filterClient.Lastname},
                    new SqlParameter { ParameterName = "@DriverLicenceNumber", Value = String.IsNullOrEmpty(filterClient.DriverLicenceNumber) ? DBNull.Value : filterClient.DriverLicenceNumber},
                    new SqlParameter { ParameterName = "@PersonalIdcardNumber", Value = String.IsNullOrEmpty(filterClient.PersonalIdcardNumber) ? DBNull.Value : filterClient.PersonalIdcardNumber},
                    new SqlParameter { ParameterName = "@Birthdate", Value = filterClient.Birthdate == null ? DBNull.Value : filterClient.Birthdate},
                    new SqlParameter { ParameterName = "@Gender", Value = String.IsNullOrEmpty(filterClient.Gender) ? DBNull.Value : filterClient.Gender},
                };

                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Parameters.AddRange(sqlParams.ToArray());
                cmd.Connection.Open();

                countOfClients = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return countOfClients;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public int CreateClient(ClientDTO client)
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
                    new SqlParameter { ParameterName = "@PersonalIdcardNumber", Value = String.IsNullOrEmpty(client.PersonalIdcardNumber) ? DBNull.Value : client.PersonalIdcardNumber},
                    new SqlParameter { ParameterName = "@Birthdate", Value = client.Birthdate == null ? DBNull.Value : client.Birthdate},
                    new SqlParameter { ParameterName = "@Gender", Value = String.IsNullOrEmpty(client.Gender) ? DBNull.Value : client.Gender}
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

        public IEnumerable<ClientDTO> GetAllClients(int pageNumber = 1, int rowsPerPage = 10)
        {
            string sqlCommand = "EXEC dbo.GetAllClients @PageNumber, @RowsPerPage";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@PageNumber", Value = pageNumber },
                    new SqlParameter { ParameterName = "@RowsPerPage", Value = rowsPerPage }
                };

                IEnumerable<Client> clients = _context.Clients.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable();
                return clients.Select(x => Mapper.ToClientDTO(x));
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public ClientDTO GetClientById(int id)
        {
            string sqlCommand = "EXEC dbo.GetClientById @ClientId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@ClientId", Value = id}
                };

                Client? client = _context.Clients.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable().FirstOrDefault();
                if (client == null)
                {
                    throw new Exception($"Client with ID {id} was not found!");
                }

                return Mapper.ToClientDTO(client);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public IEnumerable<ClientDTO> GetClientsByFilters(ClientDTO client, int pageNumber = 1, int rowsPerPage = 10)
        {
            string sqlCommand = "EXEC dbo.GetClientsByFilters @Firstname, @Lastname, @DriverLicenceNumber, @PersonalIDCardNumber, @Birthdate, @Gender, @PageNumber, @RowsPerPage";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@Firstname", Value = String.IsNullOrEmpty(client.Firstname) ? DBNull.Value : client.Firstname},
                    new SqlParameter { ParameterName = "@Lastname", Value = String.IsNullOrEmpty(client.Lastname) ? DBNull.Value : client.Lastname},
                    new SqlParameter { ParameterName = "@DriverLicenceNumber", Value = String.IsNullOrEmpty(client.DriverLicenceNumber) ? DBNull.Value : client.DriverLicenceNumber},
                    new SqlParameter { ParameterName = "@PersonalIdcardNumber", Value = String.IsNullOrEmpty(client.PersonalIdcardNumber) ? DBNull.Value : client.PersonalIdcardNumber},
                    new SqlParameter { ParameterName = "@Birthdate", Value = client.Birthdate == null ? DBNull.Value : client.Birthdate},
                    new SqlParameter { ParameterName = "@Gender", Value = String.IsNullOrEmpty(client.Gender) ? DBNull.Value : client.Gender},
                    new SqlParameter { ParameterName = "@PageNumber", Value = pageNumber },
                    new SqlParameter { ParameterName = "@RowsPerPage", Value = rowsPerPage }
                };

                IEnumerable<Client> clients = _context.Clients.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable();
                return clients.Select(x => Mapper.ToClientDTO(x));
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public void UpdateClient(ClientDTO client)
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
                    new SqlParameter { ParameterName = "@PersonalIdcardNumber", Value = String.IsNullOrEmpty(client.PersonalIdcardNumber) ? "": client.PersonalIdcardNumber},
                    new SqlParameter { ParameterName = "@Birthdate", Value = client.Birthdate == null ? DBNull.Value : client.Birthdate},
                    new SqlParameter { ParameterName = "@Gender", Value = String.IsNullOrEmpty(client.Gender) ? "" : client.Gender}
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