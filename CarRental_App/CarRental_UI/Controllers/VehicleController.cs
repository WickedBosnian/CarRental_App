using CarRental_Application.Common;
using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
using CarRental_Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_UI.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private IVehicleTypeRepository _vehicleTypeRepository;
        private IVehicleManufacturerRepository _vehicleManufacturerRepository;
        private readonly int RowsPerPage = 2;
        private IEnumerable<VehicleDTO> GlobalVehicles;

        public VehicleController(IVehicleRepository vehicleRepository, IVehicleTypeRepository vehicleTypeRepository, IVehicleManufacturerRepository vehicleManufacturerRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _vehicleManufacturerRepository = vehicleManufacturerRepository;
            GlobalVehicles = new List<VehicleDTO>();
        }

        private int CountOfVehicles()
        {
            return _vehicleRepository.CountOfVehicles();
        }

        /// <summary>
        /// Method that calls CountOfVehiclesWithFilters service which returns number of records from Vehicle table based on filters
        /// </summary>
        /// <param name="filterVehicle">VehicleDTO object that holds filters</param>
        /// <returns></returns>
        private int CountOfVehiclesWithFilters(VehicleDTO filterVehicle)
        {
            return _vehicleRepository.CountOfVehiclesWithFilters(filterVehicle);
        }

        /// <summary>
        /// Index action, calls services for getting vehicles from DB and handles pagination
        /// </summary>
        /// <param name="pageNumber">Current page in pagination</param>
        /// <param name="filterOn">If filterOn is true then SearchVehicles is called for getting vehicles otherwise GetVehiclesForPagination is called for getting vehicles</param>
        /// <param name="vehicleCount"></param>
        /// <returns></returns>
        public ActionResult Index(int pageNumber = 1, bool filterOn = false, int rowsCount = 0)
        {
            try
            {
                //Gets vehicle types and manufacturers for filters
                ViewData["VehicleTypes"] = _vehicleTypeRepository.GetAllVehicleTypes();
                ViewData["VehicleManufacturers"] = _vehicleManufacturerRepository.GetAllVehicleManufacturers();

                //Checks if count of rows should be updated from DB
                if (rowsCount == 0)
                {
                    rowsCount = CountOfVehicles();

                    if (rowsCount == 0)
                    {
                        return View(new VehicleDTO());
                    }
                }

                //Calculates maximum number of pages
                this.ViewBag.MaxPage = (rowsCount / RowsPerPage) - (rowsCount % RowsPerPage == 0 ? 1 : 0) + 1;
                this.ViewBag.PageNumber = pageNumber;
                this.ViewBag.RowsCount = rowsCount;

                if (filterOn)
                {
                    ISession session = HttpContext.Session;
                    VehicleDTO filterVehicle = SetFilterVehicleFromSearchFiltersInSession();

                    if (CommonFunctions.ArePropertiesNull(filterVehicle))
                    {
                        return View(GlobalVehicles);
                    }

                    this.ViewBag.FilterOn = filterOn;
                    GlobalVehicles = _vehicleRepository.SearchVehicles(filterVehicle, pageNumber, RowsPerPage);
                    this.ViewData["FilterVehicle"] = filterVehicle;
                }
                else
                {
                    GlobalVehicles = _vehicleRepository.GetVehiclesForPagination(pageNumber, RowsPerPage);
                }

                return View(GlobalVehicles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Retreives details of vehicle with parameter id, also retreives records Vehicle Type and Vehicle Manufacturer for this vehicle
        /// </summary>
        /// <param name="id">ID of the vehicle</param>
        /// <returns>Details View</returns>
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vehicle = _vehicleRepository.GetVehicleById((int)id);

                if(vehicle == null)
                {
                    return NotFound();
                }

                if (vehicle.VehicleTypeId != null)
                {
                    vehicle.VehicleType = _vehicleTypeRepository.GetVehicleTypeById((int)vehicle.VehicleTypeId);
                }

                if (vehicle.VehicleManufacturerId != null)
                {
                    vehicle.VehicleManufacturer = _vehicleManufacturerRepository.GetVehicleManufacturerById((int)vehicle.VehicleManufacturerId);
                }

                return View(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// This action calls SearchVehicles service that returns records from Vehicle table based on search filters
        /// </summary>
        /// <param name="filterVehicle">VehicleDTO object that holds filter values</param>
        /// <returns>If the filter values in VehicleDTO object are all null then it returns empty index page, otherwise it returns records from Vehicle table based on filter values</returns>
        public IActionResult SearchVehicles(VehicleDTO filterVehicle)
        {
            try
            {
                ViewData["VehicleTypes"] = _vehicleTypeRepository.GetAllVehicleTypes();
                ViewData["VehicleManufacturers"] = _vehicleManufacturerRepository.GetAllVehicleManufacturers();

                if (CommonFunctions.ArePropertiesNull(filterVehicle))
                {
                    this.ViewBag.MaxPage = 1;
                    this.ViewBag.PageNumber = 1;
                    this.ViewBag.RowsCount = 0;

                    return View("Index", GlobalVehicles);
                }

                GlobalVehicles = _vehicleRepository.SearchVehicles(filterVehicle, 1, RowsPerPage);

                SetSearchFiltersInSession(filterVehicle);

                int rowsCount = CountOfVehiclesWithFilters(filterVehicle);

                this.ViewBag.MaxPage = (rowsCount / RowsPerPage) - (rowsCount % RowsPerPage == 0 ? 1 : 0) + 1;
                this.ViewBag.PageNumber = 1;
                this.ViewBag.RowsCount = rowsCount;
                this.ViewBag.FilterOn = true;
                this.ViewData["FilterVehicle"] = filterVehicle;

                return View("Index", GlobalVehicles);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message + " ; " + ex.InnerException?.Message);
            }
        }

        public IActionResult Create()
        {
            try
            {
                ViewData["VehicleTypes"] = _vehicleTypeRepository.GetAllVehicleTypes();
                ViewData["VehicleManufacturers"] = _vehicleManufacturerRepository.GetAllVehicleManufacturers();

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " ; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls CreateVehicle service that returns an ID of created record and then it opens details page of the created record
        /// </summary>
        /// <param name="vehicle">DTO object of a vehicle</param>
        /// <returns>Details View if CreateVehicle was successful otherwise throws an error page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleDTO vehicle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int vehicleId = _vehicleRepository.CreateVehicle(vehicle);
                    return RedirectToAction("Details", new { id = vehicleId });
                }
                return View(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls GetVehicleById service which returns a vehicle record based on ID and then returns a view for editing vehicles
        /// </summary>
        /// <param name="id">ID of the vehicle record</param>
        /// <returns>Edit View</returns>
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vehicle = _vehicleRepository.GetVehicleById((int)id);

                if (vehicle == null)
                {
                    return NotFound();
                }

                if (vehicle.VehicleTypeId != null)
                {
                    vehicle.VehicleType = _vehicleTypeRepository.GetVehicleTypeById((int)vehicle.VehicleTypeId);
                }

                if (vehicle.VehicleManufacturerId != null)
                {
                    vehicle.VehicleManufacturer = _vehicleManufacturerRepository.GetVehicleManufacturerById((int)vehicle.VehicleManufacturerId);
                }

                ViewData["VehicleTypes"] = _vehicleTypeRepository.GetAllVehicleTypes();
                ViewData["VehicleManufacturers"] = _vehicleManufacturerRepository.GetAllVehicleManufacturers();

                return View(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls UpdateVehicle service which updates vehicle based on the sent parameters and then redirects to details page of the updated vehicle
        /// </summary>
        /// <param name="id">ID of the vehicle record for updating</param>
        /// <param name="vehicle">DTO object for the vehicle with new data</param>
        /// <returns>Details Action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, VehicleDTO vehicle)
        {
            try
            {
                if (id != vehicle.VehicleId || id == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _vehicleRepository.UpdateVehicle(vehicle);

                    return RedirectToAction("Details", new { id = id });
                }

                return View(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Gets the vehicle for deletion by ID and then opens page for deletion
        /// </summary>
        /// <param name="id">ID of the vehicle for deletion</param>
        /// <returns>Delete Vliew</returns>
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vehicle = _vehicleRepository.GetVehicleById((int)id);

                if (vehicle == null)
                {
                    return NotFound();
                }

                if (vehicle.VehicleTypeId != null)
                {
                    vehicle.VehicleType = _vehicleTypeRepository.GetVehicleTypeById((int)vehicle.VehicleTypeId);
                    ViewData["VehicleType"] = vehicle.VehicleType;
                }

                if (vehicle.VehicleManufacturerId != null)
                {
                    vehicle.VehicleManufacturer = _vehicleManufacturerRepository.GetVehicleManufacturerById((int)vehicle.VehicleManufacturerId);
                    ViewData["VehicleManufacturer"] = vehicle.VehicleManufacturer;
                }

                return View(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls service DeleteVehicle that deletes a vehicle with ID that matches parameter id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

                int deletedVehicleId = _vehicleRepository.DeleteVehicle((int)id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        private VehicleDTO SetFilterVehicleFromSearchFiltersInSession()
        {
            ISession session = HttpContext.Session;

            VehicleDTO filterVehicle = new VehicleDTO();
            filterVehicle.VehicleName = session.GetString("filter_VehicleName");
            filterVehicle.VehicleManufacturerId = !String.IsNullOrEmpty(session.GetString("filter_VehicleManufacturerId")) ? Convert.ToInt32(session.GetString("filter_VehicleManufacturerId")) : null;
            filterVehicle.VehicleTypeId = !String.IsNullOrEmpty(session.GetString("filter_filterVehicleTypeId")) ? Convert.ToInt32(session.GetString("filter_filterVehicleTypeId")) : null;

            return filterVehicle;
        }

        /// <summary>
        /// Sets session filter parameters
        /// </summary>
        /// <param name="filterVehicle">VehicleDTO object that holds filter values</param>
        private void SetSearchFiltersInSession(VehicleDTO filterVehicle)
        {
            ISession session = HttpContext.Session;
            session.SetString("filter_VehicleName", String.IsNullOrEmpty(filterVehicle.VehicleName) ? "" : filterVehicle.VehicleName);
#pragma warning disable CS8604 // Possible null reference argument. It can't be null, it will either be a valid value or an empty string.
            session.SetString("filter_VehicleManufacturerId", Convert.ToString(filterVehicle.VehicleManufacturerId));
            session.SetString("filter_filterVehicleTypeId", Convert.ToString(filterVehicle.VehicleTypeId));
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
