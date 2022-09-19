using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleManufacturerController : ControllerBase
    {
        private IVehicleManufacturerRepository _vehicleManufacturerRepository;
        public VehicleManufacturerController(IVehicleManufacturerRepository vehicleManufacturerRepository)
        {
            _vehicleManufacturerRepository = vehicleManufacturerRepository;
        }

        // GET: api/<VehicleManufacturerController>
        [HttpGet]
        public ActionResult<IEnumerable<VehicleManufacturerDTO>> Get()
        {
            try
            {
                return Ok(_vehicleManufacturerRepository.GetAllVehicleManufacturers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // GET api/<VehicleManufacturerController>/5
        [HttpGet("{id}")]
        public ActionResult<VehicleManufacturerDTO> Get(int id)
        {
            try
            {
                return Ok(_vehicleManufacturerRepository.GetVehicleManufacturerById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // POST api/<VehicleManufacturerController>
        [HttpPost]
        public ActionResult<int> Post(VehicleManufacturerDTO vehicleManufacturer)
        {
            try
            {
                int vehicleManufacturerId = _vehicleManufacturerRepository.CreateVehicleManufacturer(vehicleManufacturer);
                if (vehicleManufacturerId == -1)
                {
                    throw new Exception("There was an error. Client was not created.");
                }

                return Ok(vehicleManufacturerId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // PUT api/<VehicleManufacturerController>/5
        [HttpPut]
        public ActionResult Put(VehicleManufacturerDTO vehicleManufacturer)
        {
            try
            {
                _vehicleManufacturerRepository.UpdateVehicleManufacturer(vehicleManufacturer);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // DELETE api/<VehicleManufacturerController>/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                return _vehicleManufacturerRepository.DeleteVehicleManufacturer(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }
    }
}
