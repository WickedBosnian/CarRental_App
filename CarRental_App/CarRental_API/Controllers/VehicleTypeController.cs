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
    public class VehicleTypeController : ControllerBase
    {
        private IVehicleTypeServices _vehicleTypeServices;
        public VehicleTypeController(IVehicleTypeServices vehicleTypeServices)
        {
            _vehicleTypeServices = vehicleTypeServices;
        }
        // GET: api/<VehicleTypeController>
        [HttpGet]
        public ActionResult<IEnumerable<VehicleType>> Get()
        {
            try
            {
                return Ok(_vehicleTypeServices.GetAllVehicleTypes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // GET api/<VehicleTypeController>/5
        [HttpGet("{id}")]
        public ActionResult<VehicleType> Get(int id)
        {
            try
            {
                VehicleType vehicleType = _vehicleTypeServices.GetVehicleTypeById(id);

                return Ok(vehicleType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // POST api/<VehicleTypeController>
        [HttpPost]
        public ActionResult<int> Post(VehicleType vehicleType)
        {
            try
            {
                int reservationId = _vehicleTypeServices.CreateVehicleType(vehicleType);
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
        public ActionResult Put(VehicleType vehicleType)
        {
            try
            {
                _vehicleTypeServices.UpdateVehicleType(vehicleType);
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
                return _vehicleTypeServices.DeleteVehicleType(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }
    }
}
