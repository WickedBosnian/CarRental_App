using CarRental_Application.Repositories;
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
            string sqlCommand = $"EXEC dbo.CreateClient @Firstname, @Lastname, @DriverLicenceNumber, @PersonalIdcardNumber, @Birthdate, @Gender";
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

            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                var clientId = sdr["ClientID"].ToString();
                createdClientId = Convert.ToInt32(clientId);
            }

            cmd.Connection.Close();

            return createdClientId;
        }

        public int DeleteClient(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _context.Clients.FromSqlRaw($"EXEC dbo.GetAllClients").AsEnumerable();
        }

        public Client GetClientById(int id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@ClientId", Value = id}
            };

            return _context.Clients.FromSqlRaw($"EXEC dbo.GetClientById @ClientId", sqlParams.ToArray()).AsEnumerable().First();
        }

        public IEnumerable<Client> GetClientsByFilters()
        {
            throw new NotImplementedException();
        }

        public void UpdateClient(Client client)
        {
            string sqlCommand = $"EXEC dbo.UpdateClient @ClientId, @Firstname, @Lastname, @DriverLicenceNumber, @PersonalIdcardNumber, @Birthdate, @Gender";
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
    }
}

//string sqlCommand = $"EXEC dbo.GetAllClients";
//SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));
//cmd.Connection.Open();
//SqlDataReader sdr = cmd.ExecuteReader();
//List<Client> clients = new List<Client>();
//var dataTable = new DataTable();
//dataTable.Load(sdr);

//if(dataTable.Rows.Count > 0)
//{
//    var serializedClients = JsonConvert.SerializeObject(dataTable);
//    clients = (List<Client>)JsonConvert.DeserializeObject(serializedClients, typeof(List<Client>));
//}

//cmd.Connection.Close();

//return clients;