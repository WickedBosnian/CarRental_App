using CarRental_Domain.Entities;
using CarRental_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Repositories
{
    public interface IReservationRepository
    {
        IEnumerable<ReservationDTO> GetAllReservations();
        IEnumerable<ReservationDTO> SearchReservations(ReservationDTO reservation);
        ReservationDTO GetReservationById(int id);
        int CreateReservation(ReservationDTO reservation);
        void CancelReservation(int id);
    }
}
