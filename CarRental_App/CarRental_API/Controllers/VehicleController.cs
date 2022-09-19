using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private IVehicleRepository _vehicleRepository;
        private IVehicleTypeRepository _vehicleTypeRepository;
        private IVehicleManufacturerRepository _vehicleManufacturerRepository;

        public VehicleController(IVehicleRepository vehicleRepository, IVehicleTypeRepository vehicleTypeRepository, IVehicleManufacturerRepository vehicleManufacturerRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _vehicleManufacturerRepository = vehicleManufacturerRepository;
        }

        // GET: api/<VehicleController>
        [HttpGet]
        public ActionResult<IEnumerable<VehicleDTO>> Get()
        {
            try
            {
                return Ok(_vehicleRepository.GetAllVehicles());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        [HttpGet("SearchVehicles")]
        public ActionResult<List<VehicleDTO>> SearchVehicles(string? vehicleName, int? vehicleManufacturerId, int? vehicleTypeId)
        {
            try
            {
                return Ok(_vehicleRepository.SearchVehicles(vehicleName, vehicleManufacturerId, vehicleTypeId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // GET api/<VehicleController>/5
        [HttpGet("{id}")]
        public ActionResult<VehicleDTO> Get(int id)
        {
            try
            {
                VehicleDTO vehicle = _vehicleRepository.GetVehicleById(id);

                vehicle.VehicleType = _vehicleTypeRepository.GetVehicleTypeById((int)vehicle.VehicleTypeId);
                vehicle.VehicleManufacturer = _vehicleManufacturerRepository.GetVehicleManufacturerById((int)vehicle.VehicleManufacturerId);

                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // POST api/<VehicleController>
        [HttpPost]
        public ActionResult<int> Post(VehicleDTO vehicle)
        {
            try
            {
                int clientId = _vehicleRepository.CreateVehicle(vehicle);
                if (clientId == -1)
                {
                    throw new Exception("There was an error. Vehicle was not created.");
                }

                return Ok(clientId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // PUT api/<VehicleController>/5
        [HttpPut]
        public ActionResult Put(VehicleDTO vehicle)
        {
            try
            {
                _vehicleRepository.UpdateVehicle(vehicle);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // DELETE api/<VehicleController>/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                return _vehicleRepository.DeleteVehicle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }
    }
}
