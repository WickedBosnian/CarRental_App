using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
using CarRental_Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_UI.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private List<VehicleTypeDTO> GlobalVehicleTypes = new List<VehicleTypeDTO>();

        public VehicleTypeController(IVehicleTypeRepository vehicleTypeRepository, IVehicleRepository vehicleRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _vehicleRepository = vehicleRepository;
        }

        // GET: VehicleTypeController
        public ActionResult Index()
        {
            try
            {
                GlobalVehicleTypes = _vehicleTypeRepository.GetAllVehicleTypes().ToList();
                return View(GlobalVehicleTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // GET: VehicleTypeController/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vehicleType = _vehicleTypeRepository.GetVehicleTypeById((int)id);

                if (vehicleType == null)
                {
                    return NotFound();
                }

                return View(vehicleType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // GET: VehicleTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleTypeDTO vehicleType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int vehicleTypeId = _vehicleTypeRepository.CreateVehicleType(vehicleType);
                    return RedirectToAction("Details", new { id = vehicleTypeId });
                }
                return View(vehicleType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // GET: VehicleTypeController/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vehicleType = _vehicleTypeRepository.GetVehicleTypeById((int)id);
                if (vehicleType == null)
                {
                    return NotFound();
                }

                return View(vehicleType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // POST: VehicleTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VehicleTypeDTO vehicleType)
        {
            try
            {
                if (id != vehicleType.VehicleTypeId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _vehicleTypeRepository.UpdateVehicleType(vehicleType);

                    return RedirectToAction("Details", new { id = id });
                    //return Redirect($"Client/Details/{id}");
                }

                return View(vehicleType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // GET: VehicleTypeController/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vehicleType = _vehicleTypeRepository.GetVehicleTypeById((int)id);

                if (vehicleType == null)
                {
                    return NotFound();
                }

                return View(vehicleType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // POST: VehicleTypeController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (VehicleTypeHasVehicle((int)id))
                {
                    var vehicleType = _vehicleTypeRepository.GetVehicleTypeById((int)id);

                    if (vehicleType == null)
                    {
                        return NotFound();
                    }

                    ViewBag.IsValid = false;
                    return View("Delete", vehicleType);
                }

                int deletedClientId = _vehicleTypeRepository.DeleteVehicleType((int)id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Checks if any vehicles exist with passed vehicleTypeId
        /// </summary>
        /// <param name="vehicleTypeId">ID of vehicle type for deletion</param>
        /// <returns>true if vehicle exists, otherwise false</returns>
        private bool VehicleTypeHasVehicle(int vehicleTypeId)
        {
            VehicleDTO validationVehicle = new VehicleDTO() { VehicleTypeId = vehicleTypeId };
            IEnumerable<VehicleDTO> vehicles = _vehicleRepository.SearchVehicles(validationVehicle, 1, 1);

            return vehicles.Count() >= 1;
        }
    }
}
