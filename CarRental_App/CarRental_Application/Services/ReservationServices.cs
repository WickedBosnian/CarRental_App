using CarRental_Application.Interfaces;
using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Services
{
    public class ReservationServices : IReservationServices
    {
        private IReservationRepository _reservationRepository;
        public ReservationServices(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public void CancelReservation(int id)
        {
            _reservationRepository.CancelReservation(id);
        }

        public int CreateReservation(Reservation reservation)
        {
            return _reservationRepository.CreateReservation(reservation);
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
        }

        public Reservation GetReservationById(int id)
        {
            return _reservationRepository.GetReservationById(id);
        }

        public IEnumerable<Reservation> SearchReservations(DateTime? dateFrom, DateTime? dateTo, int? clientId, int? vehicleId, bool? active)
        {
            return _reservationRepository.SearchReservations(dateFrom, dateTo, clientId, vehicleId, active);
        }
    }
}
