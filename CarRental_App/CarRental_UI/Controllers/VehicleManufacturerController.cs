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
        private List<VehicleManufacturerDTO> GlobalVehicleManufacturers = new List<VehicleManufacturerDTO>();

        public VehicleManufacturerController(IVehicleManufacturerRepository vehicleManufacturerRepository)
        {
            _vehicleManufacturerRepository = vehicleManufacturerRepository;
        }

        // GET: VehicleManufacturerController
        public ActionResult Index()
        {
            try
            {
                GlobalVehicleManufacturers = _vehicleManufacturerRepository.GetAllVehicleManufacturers().ToList();
                return View(GlobalVehicleManufacturers);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, VehicleManufacturerDTO vehicleManufacturer)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                int deletedClientId = _vehicleManufacturerRepository.DeleteVehicleManufacturer((int)id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }
    }
}
