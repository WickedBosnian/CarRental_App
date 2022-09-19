using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_Domain.Enums;
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
    public class ReservationRepository : IReservationRepository
    {
        private CarRentalDBContext _context;

        public ReservationRepository(CarRentalDBContext context)
        {
            _context = context;
        }

        public void CancelReservation(int id)
        {
            string sqlCommand = "EXEC dbo.CancelReservation @ReservationId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@ReservationId", Value = id}
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

        public int CreateReservation(Reservation reservation)
        {
            int createdReservationId = -1;
            string sqlCommand = "EXEC dbo.CreateReservation @ClientId, @VehicleID, @ReservationDateFrom, @ReservationDateTo";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@ClientId", Value = reservation.ClientId},
                    new SqlParameter { ParameterName = "@VehicleID", Value = reservation.VehicleID},
                    new SqlParameter { ParameterName = "@ReservationDateFrom", Value = reservation.ReservationDateFrom},
                    new SqlParameter { ParameterName = "@ReservationDateTo", Value = reservation.ReservationDateTo}
                };

                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Parameters.AddRange(sqlParams.ToArray());
                cmd.Connection.Open();

                createdReservationId = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return createdReservationId;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public int DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            string sqlCommand = $"EXEC dbo.GetAllReservations";

            try
            {
                return _context.Reservations.FromSqlRaw(sqlCommand);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public Reservation GetReservationById(int id)
        {
            string sqlCommand = $"EXEC dbo.GetReservationById @ReservationId";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@ReservationId", Value = id}
                };

                Reservation? reservation = _context.Reservations.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable().FirstOrDefault();
                if(reservation == null)
                {
                    throw new Exception($"Reservation with ID {id} was not found!");
                }

                return reservation;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public IEnumerable<Reservation> SearchReservations(DateTime? dateFrom, DateTime? dateTo, int? clientId, int? vehicleId, bool? active)
        {
            string sqlCommand = "EXEC dbo.SearchReservations @DateFrom, @DateTo, @ClientId, @VehicleId, @Active";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@DateFrom", Value = dateFrom == null ? DBNull.Value : dateFrom},
                    new SqlParameter { ParameterName = "@DateTo", Value = dateTo == null ? DBNull.Value : dateTo},
                    new SqlParameter { ParameterName = "@ClientId", Value = clientId == null ? DBNull.Value : clientId},
                    new SqlParameter { ParameterName = "@VehicleId", Value = vehicleId == null ? DBNull.Value : vehicleId},
                    new SqlParameter { ParameterName = "@Active", Value = active == null ? DBNull.Value : active}
                };

                return _context.Reservations.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }
    }
}
