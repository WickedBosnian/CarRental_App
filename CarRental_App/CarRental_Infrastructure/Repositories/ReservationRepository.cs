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

        public int CreateReservation(ReservationDTO reservation)
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

        public IEnumerable<ReservationDTO> GetAllReservations()
        {
            string sqlCommand = $"EXEC dbo.GetAllReservations";

            try
            {
                IEnumerable<Reservation> reservations = _context.Reservations.FromSqlRaw(sqlCommand);
                return reservations.Select(x => Mapper.ToReservationDTO(x));
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public IEnumerable<ReservationDTO> GetReservationsForPagination(int pageNumber = 1, int rowsPerPage = 10)
        {
            string sqlCommand = "EXEC dbo.GetReservationsForPagination @PageNumber, @RowsPerPage";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@PageNumber", Value = pageNumber },
                    new SqlParameter { ParameterName = "@RowsPerPage", Value = rowsPerPage }
                };

                IEnumerable<Reservation> reservations = _context.Reservations.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable();
                return reservations.Select(x => Mapper.ToReservationDTO(x));
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public ReservationDTO GetReservationById(int id)
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

                return Mapper.ToReservationDTO(reservation);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public IEnumerable<ReservationDTO> SearchReservations(ReservationDTO reservation, int pageNumber = 1, int rowsPerPage = 10)
        {
            string sqlCommand = "EXEC dbo.SearchReservations @DateFrom, @DateTo, @ClientId, @VehicleId, @Active, @PageNumber, @RowsPerPage";

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@DateFrom", Value = reservation.ReservationDateFrom == null ? DBNull.Value : reservation.ReservationDateFrom},
                    new SqlParameter { ParameterName = "@DateTo", Value = reservation.ReservationDateTo == null ? DBNull.Value : reservation.ReservationDateTo},
                    new SqlParameter { ParameterName = "@ClientId", Value = reservation.ClientId == null ? DBNull.Value : reservation.ClientId},
                    new SqlParameter { ParameterName = "@VehicleId", Value = reservation.VehicleID == null ? DBNull.Value : reservation.VehicleID},
                    new SqlParameter { ParameterName = "@Active", Value = reservation.Active == null ? DBNull.Value : reservation.Active},
                    new SqlParameter { ParameterName = "@PageNumber", Value = pageNumber},
                    new SqlParameter { ParameterName = "@RowsPerPage", Value = rowsPerPage}
                };

                IEnumerable<Reservation> reservations = _context.Reservations.FromSqlRaw(sqlCommand, sqlParams.ToArray()).AsEnumerable();
                return reservations.Select(x => Mapper.ToReservationDTO(x));
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public int CountOfReservations()
        {
            int countOfReservations = -1;
            string sqlCommand = "SELECT dbo.CountOfReservations()";

            try
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Connection.Open();

                countOfReservations = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return countOfReservations;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }

        public int CountOfReservationsWithFilters(ReservationDTO filterReservation)
        {
            string sqlCommand = "SELECT dbo.CountOfReservationsWithFilters(@DateFrom, @DateTo, @ClientId, @VehicleId, @Active)";
            int countOfReservations = -1;
            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@DateFrom", Value = filterReservation.ReservationDateFrom == null ? DBNull.Value : filterReservation.ReservationDateFrom},
                    new SqlParameter { ParameterName = "@DateTo", Value = filterReservation.ReservationDateTo == null ? DBNull.Value : filterReservation.ReservationDateTo},
                    new SqlParameter { ParameterName = "@ClientId", Value = filterReservation.ClientId == null ? DBNull.Value : filterReservation.ClientId},
                    new SqlParameter { ParameterName = "@VehicleId", Value = filterReservation.VehicleID == null ? DBNull.Value : filterReservation.VehicleID},
                    new SqlParameter { ParameterName = "@Active", Value = filterReservation.Active == null ? DBNull.Value : filterReservation.Active},
                };

                SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(_context.Database.GetConnectionString()));

                cmd.Parameters.AddRange(sqlParams.ToArray());
                cmd.Connection.Open();

                countOfReservations = (int)cmd.ExecuteScalar();

                cmd.Connection.Close();

                return countOfReservations;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message + ";" + sqlEx.InnerException?.Message);
            }
        }
    }
}
