using CarRental_Application.Interfaces;
using CarRental_Application.Services;
using CarRental_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleManufacturerController : ControllerBase
    {
        private IVehicleManufacturerServices _vehicleManufacturerServices;
        public VehicleManufacturerController(IVehicleManufacturerServices vehicleManufacturerServices)
        {
            _vehicleManufacturerServices = vehicleManufacturerServices;
        }

        // GET: api/<VehicleManufacturerController>
        [HttpGet]
        public ActionResult<IEnumerable<VehicleManufacturer>> Get()
        {
            try
            {
                return Ok(_vehicleManufacturerServices.GetAllVehicleManufacturers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // GET api/<VehicleManufacturerController>/5
        [HttpGet("{id}")]
        public ActionResult<VehicleManufacturer> Get(int id)
        {
            try
            {
                return Ok(_vehicleManufacturerServices.GetVehicleManufacturerById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // POST api/<VehicleManufacturerController>
        [HttpPost]
        public ActionResult<int> Post(VehicleManufacturer vehicleManufacturer)
        {
            try
            {
                int vehicleManufacturerId = _vehicleManufacturerServices.CreateVehicleManufacturer(vehicleManufacturer);
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
        [HttpPut("{id}")]
        public ActionResult Put(VehicleManufacturer vehicleManufacturer)
        {
            try
            {
                _vehicleManufacturerServices.UpdateVehicleManufacturer(vehicleManufacturer);
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
                return _vehicleManufacturerServices.DeleteVehicleManufacturer(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }
    }
}
