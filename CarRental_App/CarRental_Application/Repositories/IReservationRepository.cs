using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Repositories
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAllReservations();
    }
}
