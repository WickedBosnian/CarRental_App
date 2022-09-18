using CarRental_Application.Interfaces;
using CarRental_Application.Repositories;
using CarRental_Application.Services;
using CarRental_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationServices _reservationServices;
        private IClientServices _clientServices;
        public ReservationController(IReservationServices reservationServices, IClientServices clientServices)
        {
            _reservationServices = reservationServices;
            _clientServices = clientServices;
        }
        // GET: api/<ReservationController>
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get()
        {
            try
            {
                return Ok(_reservationServices.GetAllReservations());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        [HttpGet("SearchReservations")]
        public ActionResult<List<Client>> SearchReservations(DateTime? dateFrom, DateTime? dateTo, int? clientId, int? vehicleId, bool? active)
        {
            try
            {
                return Ok(_reservationServices.SearchReservations(dateFrom, dateTo, clientId, vehicleId, active));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // GET api/<ReservationController>/5
        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            try
            {
                Reservation reservation = _reservationServices.GetReservationById(id);
                reservation.Client = _clientServices.GetClientById(reservation.ClientId);

                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // POST api/<ReservationController>
        [HttpPost]
        public ActionResult<int> Post(Reservation reservation)
        {
            try
            {
                int reservationId = _reservationServices.CreateReservation(reservation);
                if (reservationId == -1)
                {
                    throw new Exception("There was an error. Reservation was not created.");
                }

                return Ok(reservationId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // DELETE api/<ReservationController>/5
        [HttpPut("CancelReservation")]
        public ActionResult CancelReservation(int id)
        {
            try
            {
                _reservationServices.CancelReservation(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }
    }
}
