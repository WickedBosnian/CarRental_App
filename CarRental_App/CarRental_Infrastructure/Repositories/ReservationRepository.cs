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
    public class ReservationRepository : IReservationRepository
    {
        private CarRentalDBContext _context;

        public ReservationRepository(CarRentalDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            string sqlCommand = $"EXEC dbo.GetAllReservations";

            return _context.Reservations.FromSqlRaw(sqlCommand);
        }
    }
}
