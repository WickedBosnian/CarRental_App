using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
using CarRental_Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_UI.Controllers
{
    public class VehicleManufacturerController : Controller
    {
        private readonly IVehicleManufacturerRepository _vehicleManufacturerRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleManufacturerController(IVehicleManufacturerRepository vehicleManufacturerRepository, IVehicleRepository vehicleRepository)
        {
            _vehicleManufacturerRepository = vehicleManufacturerRepository;
            _vehicleRepository = vehicleRepository;
        }

        // GET: VehicleManufacturerController
        public ActionResult Index()
        {
            try
            {
                return View(_vehicleManufacturerRepository.GetAllVehicleManufacturers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // GET: VehicleManufacturerController/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vehicleManufacturer = _vehicleManufacturerRepository.GetVehicleManufacturerById((int)id);

                if (vehicleManufacturer == null)
                {
                    return NotFound();
                }

                return View(vehicleManufacturer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // GET: VehicleManufacturerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleManufacturerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleManufacturerDTO vehicleManufacturer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int vehicleTypeId = _vehicleManufacturerRepository.CreateVehicleManufacturer(vehicleManufacturer);
                    return RedirectToAction("Details", new { id = vehicleTypeId });
                }
                return View(vehicleManufacturer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // GET: VehicleManufacturerController/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vehicleManufacturer = _vehicleManufacturerRepository.GetVehicleManufacturerById((int)id);
                if (vehicleManufacturer == null)
                {
                    return NotFound();
                }

                return View(vehicleManufacturer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // POST: VehicleManufacturerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, VehicleManufacturerDTO vehicleManufacturer)
        {
            try
            {
                if (id != vehicleManufacturer.VehicleManufacturerId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _vehicleManufacturerRepository.UpdateVehicleManufacturer(vehicleManufacturer);

                    return RedirectToAction("Details", new { id = id });
                    //return Redirect($"Client/Details/{id}");
                }

                return View(vehicleManufacturer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // GET: VehicleManufacturerController/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vehicleManufacturer = _vehicleManufacturerRepository.GetVehicleManufacturerById((int)id);

                if (vehicleManufacturer == null)
                {
                    return NotFound();
                }

                return View(vehicleManufacturer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        // POST: VehicleManufacturerController/Delete/5
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

                if (VehicleManufacturerHasVehicle((int)id))
                {
                    var vehicleManufacturer = _vehicleManufacturerRepository.GetVehicleManufacturerById((int)id);

                    if (vehicleManufacturer == null)
                    {
                        return NotFound();
                    }

                    ViewBag.IsValid = false;
                    return View("Delete", vehicleManufacturer);
                }

                int deletedClientId = _vehicleManufacturerRepository.DeleteVehicleManufacturer((int)id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Checks if any vehicles exist with passed vehicleManufacturerId
        /// </summary>
        /// <param name="vehicleManufacturerId">ID of vehicle manufacturer for deletion</param>
        /// <returns>true if vehicle exists, otherwise false</returns>
        private bool VehicleManufacturerHasVehicle(int vehicleManufacturerId)
        {
            VehicleDTO validationVehicle = new VehicleDTO() { VehicleManufacturerId = vehicleManufacturerId };
            IEnumerable<VehicleDTO> vehicles = _vehicleRepository.SearchVehicles(validationVehicle, 1, 1);

            return vehicles.Count() >= 1;
        }
    }
}
