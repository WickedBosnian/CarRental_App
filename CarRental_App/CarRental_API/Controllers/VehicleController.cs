using CarRental_Application.Interfaces;
using CarRental_Application.Services;
using CarRental_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private IVehicleServices _vehicleService;
        private IVehicleTypeServices _vehicleTypeServices;
        private IVehicleManufacturerServices _vehicleManufacturerServices;

        public VehicleController(IVehicleServices vehicleService, IVehicleTypeServices vehicleTypeServices, IVehicleManufacturerServices vehicleManufacturerServices)
        {
            _vehicleService = vehicleService;
            _vehicleTypeServices = vehicleTypeServices;
            _vehicleManufacturerServices = vehicleManufacturerServices;
        }

        // GET: api/<VehicleController>
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> Get()
        {
            try
            {
                return Ok(_vehicleService.GetAllVehicles());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        [HttpGet("SearchVehicles")]
        public ActionResult<List<Client>> SearchVehicles(string? vehicleName, int? vehicleManufacturerId, int? vehicleTypeId)
        {
            try
            {
                return Ok(_vehicleService.SearchVehicles(vehicleName, vehicleManufacturerId, vehicleTypeId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // GET api/<VehicleController>/5
        [HttpGet("{id}")]
        public ActionResult<Client> Get(int id)
        {
            try
            {
                Vehicle vehicle = _vehicleService.GetVehicleById(id);

                vehicle.VehicleType = _vehicleTypeServices.GetVehicleTypeById((int)vehicle.VehicleTypeId);
                vehicle.VehicleManufacturer = _vehicleManufacturerServices.GetVehicleManufacturerById((int)vehicle.VehicleManufacturerId);

                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // POST api/<VehicleController>
        [HttpPost]
        public ActionResult<int> Post(Vehicle vehicle)
        {
            try
            {
                int clientId = _vehicleService.CreateVehicle(vehicle);
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
        public ActionResult Put(Vehicle vehicle)
        {
            try
            {
                _vehicleService.UpdateVehicle(vehicle);
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
                return _vehicleService.DeleteVehicle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }
    }
}
