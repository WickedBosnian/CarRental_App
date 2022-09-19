using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private IVehicleTypeRepository _vehicleTypeRepository;
        public VehicleTypeController(IVehicleTypeRepository vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }
        // GET: api/<VehicleTypeController>
        [HttpGet]
        public ActionResult<IEnumerable<VehicleTypeDTO>> Get()
        {
            try
            {
                return Ok(_vehicleTypeRepository.GetAllVehicleTypes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // GET api/<VehicleTypeController>/5
        [HttpGet("{id}")]
        public ActionResult<VehicleTypeDTO> Get(int id)
        {
            try
            {
                VehicleTypeDTO vehicleType = _vehicleTypeRepository.GetVehicleTypeById(id);

                return Ok(vehicleType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // POST api/<VehicleTypeController>
        [HttpPost]
        public ActionResult<int> Post(VehicleTypeDTO vehicleType)
        {
            try
            {
                int reservationId = _vehicleTypeRepository.CreateVehicleType(vehicleType);
                if (reservationId == -1)
                {
                    throw new Exception("There was an error. Vehicle type was not created.");
                }

                return Ok(reservationId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // PUT api/<VehicleTypeController>/5
        [HttpPut]
        public ActionResult Put(VehicleTypeDTO vehicleType)
        {
            try
            {
                _vehicleTypeRepository.UpdateVehicleType(vehicleType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // DELETE api/<VehicleTypeController>/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                return _vehicleTypeRepository.DeleteVehicleType(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }
    }
}
