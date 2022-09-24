using CarRental_Application.Common;
using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
using CarRental_Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_UI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IVehicleRepository _vehicleRepository;

        private readonly int RowsPerPage = 2;
        private IEnumerable<ReservationDTO> GlobalReservation;

        public ReservationController(IReservationRepository reservationRepository, IClientRepository clientRepository, IVehicleRepository vehicleRepository)
        {
            _reservationRepository = reservationRepository;
            _clientRepository = clientRepository;
            _vehicleRepository = vehicleRepository;
            GlobalReservation = new List<ReservationDTO>();
        }


        /// <summary>
        /// Calls service CountOfReservations that returns total number of records from Reservation table
        /// </summary>
        /// <returns>Number of records in Reservation table</returns>
        private int CountOfReservations()
        {
            return _reservationRepository.CountOfReservations();
        }

        /// <summary>
        /// Returns count of records from Reservation table based on search filters
        /// </summary>
        /// <param name="filterReservation">ReservationDTO object that holds filter values</param>
        /// <returns>Count of records from Reservation table based on search filters</returns>
        private int CountOfReservationsWithFilters(ReservationDTO filterReservation)
        {
            return _reservationRepository.CountOfReservationsWithFilters(filterReservation);
        }


        /// <summary>
        /// Index action, calls GetReservationsForPagination service which retreives all clients based on page number and rows per page
        /// </summary>
        /// <param name="pageNumber">Value of current page on pagination</param>
        /// <param name="filterOn">If filterOn is true then SearchReservations is called for getting reservations otherwise GetReservationsForPagination is called for getting reservations</param>
        /// <param name="rowsCount">Total number of rows for showing in pagination</param>
        /// <returns>Index view of reservations</returns>
        public ActionResult Index(int pageNumber = 1, bool filterOn = false, int rowsCount = 0)
        {
            try
            {
                //Gets vehicles and clients for filters
                ViewData["Vehicles"] = _vehicleRepository.GetAllVehicles();
                ViewData["Clients"] = _clientRepository.GetAllClients();

                //Checks if count of clients should be updated from DB
                if (rowsCount == 0)
                {
                    rowsCount = CountOfReservations();

                    if (rowsCount == 0)
                    {
                        return View(new ReservationDTO());
                    }
                }

                //Calculates maximum number of pages
                this.ViewBag.MaxPage = (rowsCount / RowsPerPage) - (rowsCount % RowsPerPage == 0 ? 1 : 0) + 1;
                this.ViewBag.PageNumber = pageNumber;
                this.ViewBag.RowsCount = rowsCount;

                if (filterOn)
                {
                    ISession session = HttpContext.Session;
                    ReservationDTO filterReservation = SetFilterFromSearchFiltersInSession();

                    if (CommonFunctions.ArePropertiesNull(GlobalReservation))
                    {
                        return View(GlobalReservation);
                    }

                    this.ViewBag.FilterOn = filterOn;
                    GlobalReservation = _reservationRepository.SearchReservations(filterReservation, pageNumber, RowsPerPage);
                    this.ViewData["FilterReservation"] = filterReservation;
                }
                else
                {
                    GlobalReservation = _reservationRepository.GetReservationsForPagination(pageNumber, RowsPerPage);
                }

                return View(GlobalReservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Retreives details of reservation with parameter id
        /// </summary>
        /// <param name="id">ID of the reesrvation</param>
        /// <returns>Details View</returns>
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var reservation = _reservationRepository.GetReservationById((int)id);

                if (reservation == null)
                {
                    return NotFound();
                }

                if(reservation.ClientId != null)
                {
                    reservation.Client = _clientRepository.GetClientById((int)reservation.ClientId);
                }

                if (reservation.VehicleID != null)
                {
                    reservation.Vehicle = _vehicleRepository.GetVehicleById((int)reservation.VehicleID);
                }

                return View(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// This action calls SearchReservations service that returns records from Reservation table based on search filters
        /// </summary>
        /// <param name="filterReservation">ReservationDTO object that holds filter values</param>
        /// <returns>If the filter values in ReservationDTO object are all null then it returns empty index page, otherwise it returns records from Reservation table based on filter values</returns>
        public IActionResult SearchReservations(ReservationDTO filterReservation)
        {
            try
            {
                ViewData["Vehicles"] = _vehicleRepository.GetAllVehicles();
                ViewData["Clients"] = _clientRepository.GetAllClients();

                if (CommonFunctions.ArePropertiesNull(filterReservation))
                {
                    this.ViewBag.MaxPage = 1;
                    this.ViewBag.PageNumber = 1;
                    this.ViewBag.RowsCount = 0;

                    return View("Index", GlobalReservation);
                }

                GlobalReservation = _reservationRepository.SearchReservations(filterReservation, 1, RowsPerPage);

                SetSearchFiltersInSession(filterReservation);

                int rowsCount = CountOfReservationsWithFilters(filterReservation);

                this.ViewBag.MaxPage = (rowsCount / RowsPerPage) - (rowsCount % RowsPerPage == 0 ? 1 : 0) + 1;
                this.ViewBag.PageNumber = 1;
                this.ViewBag.RowsCount = rowsCount;
                this.ViewBag.FilterOn = true;
                this.ViewData["FilterReservation"] = filterReservation;

                return View("Index", GlobalReservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " ; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Returns Create view and gets all vehicles and clients
        /// </summary>
        /// <returns>Create view</returns>
        public ActionResult Create()
        {
            try
            {
                ViewData["Vehicles"] = _vehicleRepository.GetAllVehicles();
                ViewData["Clients"] = _clientRepository.GetAllClients();

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " ; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls CreateReservation service that returns an ID of created record and then it opens details page of the created record
        /// </summary>
        /// <param name="reservation">DTO object of a reservation</param>
        /// <returns>Details View if CreateReservation was successful otherwise returns an error page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationDTO reservation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int reservationId = _reservationRepository.CreateReservation(reservation);
                    return RedirectToAction("Details", new { id = reservationId });
                }
                return View(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls GetReservationById service which returns a record frome Reservation table based on ID and then returns a view for editing reservations
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

                var reservation = _reservationRepository.GetReservationById((int)id);

                if (reservation == null)
                {
                    return NotFound();
                }

                if (reservation.ClientId != null)
                {
                    reservation.Client = _clientRepository.GetClientById((int)reservation.ClientId);
                }

                if (reservation.VehicleID != null)
                {
                    reservation.Vehicle = _vehicleRepository.GetVehicleById((int)reservation.VehicleID);
                }

                ViewData["Vehicles"] = _vehicleRepository.GetAllVehicles();
                ViewData["Clients"] = _clientRepository.GetAllClients();

                return View(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Gets the reservation for cancelation by ID and then opens page for cancelation
        /// </summary>
        /// <param name="id">ID of the reservation for cancelation</param>
        /// <returns>Cancel View</returns>
        public ActionResult Cancel(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var reservation = _reservationRepository.GetReservationById((int)id);

                if (reservation == null)
                {
                    return NotFound();
                }

                if (reservation.VehicleID != null)
                {
                    reservation.Vehicle = _vehicleRepository.GetVehicleById((int)reservation.VehicleID);
                }

                if (reservation.ClientId != null)
                {
                    reservation.Client = _clientRepository.GetClientById((int)reservation.ClientId);
                }

                return View(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls CancelReservation service which sets Active to false on a reservation
        /// </summary>
        /// <param name="id">Id of record</param>
        /// <param name="reservation">ReservationDTO object of reservation for cancelation</param>
        /// <returns></returns>
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel (int? id, ReservationDTO reservation)
        {
            try
            {
                if (id != reservation.ReservationId || id == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _reservationRepository.CancelReservation((int)id);

                    return RedirectToAction("Details", new { id = id });
                }

                return View(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Sets ReservationDTO object for filtering records from Reservation table
        /// </summary>
        /// <returns>ReservationDTO object with filter values</returns>
        private ReservationDTO SetFilterFromSearchFiltersInSession()
        {
            ISession session = HttpContext.Session;

            ReservationDTO filterReservation = new ReservationDTO();
#pragma warning disable CS8604 // Possible null reference argument. It can't be a null reference because of the condition.
            filterReservation.ReservationDateFrom = String.IsNullOrEmpty(session.GetString("filter_ReservationDateFrom")) ? null : DateTime.Parse(session.GetString("filter_ReservationDateFrom"));
            filterReservation.ReservationDateTo = String.IsNullOrEmpty(session.GetString("filter_ReservationDateTo")) ? null : DateTime.Parse(session.GetString("filter_ReservationDateTo"));
#pragma warning restore CS8604 // Possible null reference argument.

            filterReservation.ClientId = String.IsNullOrEmpty(session.GetString("filter_ClientId")) ? null : Convert.ToInt32(session.GetString("filter_ClientId"));
            filterReservation.VehicleID = String.IsNullOrEmpty(session.GetString("filter_VehicleID")) ? null : Convert.ToInt32(session.GetString("filter_VehicleID"));
            filterReservation.Active = String.IsNullOrEmpty(session.GetString("filter_Active")) ? null : Convert.ToBoolean(session.GetString("filter_Active"));

            return filterReservation;
        }

        /// <summary>
        /// Sets session filter parameters
        /// </summary>
        /// <param name="filterReservation">ReservationDTO object that holds filter values</param>
        private void SetSearchFiltersInSession(ReservationDTO filterReservation)
        {
            ISession session = HttpContext.Session;

#pragma warning disable CS8604 // Possible null reference argument. It can't be null, it will either be a valid value or an empty string.
            session.SetString("filter_ReservationDateFrom", Convert.ToString(filterReservation.ReservationDateFrom));
            session.SetString("filter_ReservationDateTo", Convert.ToString(filterReservation.ReservationDateTo));
            session.SetString("filter_ClientId", Convert.ToString(filterReservation.ClientId));
            session.SetString("filter_VehicleID", Convert.ToString(filterReservation.VehicleID));
            session.SetString("filter_Active", Convert.ToString(filterReservation.Active));
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
