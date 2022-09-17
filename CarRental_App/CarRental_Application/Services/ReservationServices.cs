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
        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
        }
    }
}
