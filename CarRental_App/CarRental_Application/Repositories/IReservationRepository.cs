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
        IEnumerable<ReservationDTO> GetReservationsForPagination(int pageNumber = 1, int rowsPerPage = 10);
        IEnumerable<ReservationDTO> SearchReservations(ReservationDTO reservation, int pageNumber = 1, int rowsPerPage = 10);
        ReservationDTO GetReservationById(int id);
        int CreateReservation(ReservationDTO reservation);
        void CancelReservation(int id);
        int CountOfReservations();
        int CountOfReservationsWithFilters(ReservationDTO filterReservation);
        int CountOfActiveReservationsForClient(int clientId);
        int CountOfActiveReservationsForClientWithVehicleType(ReservationDTO filterReservation);
        bool IsVehicleReservedForPeriod(ReservationDTO reservation);
    }
}
