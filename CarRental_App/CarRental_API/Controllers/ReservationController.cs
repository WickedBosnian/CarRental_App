using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationRepository _reservationRepository;
        private IClientRepository _clientRepository;
        private IVehicleRepository _vehicleRepository;
        public ReservationController(IReservationRepository reservationRepository, IClientRepository clientRepository, IVehicleRepository vehicleRepository)
        {
            _reservationRepository = reservationRepository;
            _clientRepository = clientRepository;
            _vehicleRepository = vehicleRepository;
        }
        // GET: api/<ReservationController>
        [HttpGet]
        public ActionResult<IEnumerable<ReservationDTO>> Get()
        {
            try
            {
                return Ok(_reservationRepository.GetAllReservations());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        [HttpGet("SearchReservations")]
        public ActionResult<List<ReservationDTO>> SearchReservations(DateTime? dateFrom, DateTime? dateTo, int? clientId, int? vehicleId, bool? active)
        {
            try
            {
                return Ok(_reservationRepository.SearchReservations(dateFrom, dateTo, clientId, vehicleId, active));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // GET api/<ReservationController>/5
        [HttpGet("{id}")]
        public ActionResult<ReservationDTO> Get(int id)
        {
            try
            {
                ReservationDTO reservation = _reservationRepository.GetReservationById(id);
                reservation.Client = _clientRepository.GetClientById((int)reservation.ClientId);
                reservation.Vehicle = _vehicleRepository.GetVehicleById((int)reservation.VehicleID);

                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // POST api/<ReservationController>
        [HttpPost]
        public ActionResult<int> Post(ReservationDTO reservation)
        {
            try
            {
                int reservationId = _reservationRepository.CreateReservation(reservation);
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
                _reservationRepository.CancelReservation(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }
    }
}
