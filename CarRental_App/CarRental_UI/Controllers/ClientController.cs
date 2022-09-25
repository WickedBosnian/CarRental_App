using Microsoft.AspNetCore.Mvc;
using CarRental_Application.Repositories;
using CarRental_Application.Common;
using CarRental_DTO;
using CarRental_Domain.Entities;
using CarRental_Infrastructure.Repositories;

namespace CarRental_UI.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly int RowsPerPage = 5;
        private IEnumerable<ClientDTO> GlobalClients;

        public ClientController(IClientRepository clientRepository, IReservationRepository reservationRepository)
        {
            _clientRepository = clientRepository;
            _reservationRepository = reservationRepository;

            GlobalClients = new List<ClientDTO>();
        }

        /// <summary>
        /// Calls service CountOfClients that returns total number of records from Client table
        /// </summary>
        /// <returns>Number of records in Client table</returns>
        private int CountOfClients()
        {
            return _clientRepository.CountOfClients();
        }

        /// <summary>
        /// Returns count of records from Client table based on search filters
        /// </summary>
        /// <param name="filterClient">ClientDTO object that holds filter values</param>
        /// <returns>Count of records from Client table based on search filters</returns>
        private int CountOfClientsWithFilters(ClientDTO filterClient)
        {
            return _clientRepository.CountOfClientsWithFilters(filterClient);
        }

        /// <summary>
        /// Index action, calls GetClientsForPagination service which retreives all clients based on pge number and rows per page
        /// </summary>
        /// <param name="pageNumber">Value of current page on pagination</param>
        /// <param name="filterOn">If filterOn is true then SearchClients is called for getting clients otherwise GetClientsForPagination is called for getting clients</param>
        /// <param name="rowsCount">Total number of rows for showing in pagination</param>
        /// <returns>Index view of clients</returns>
        public IActionResult Index(int pageNumber = 1, bool filterOn = false, int rowsCount = 0)
        {
            try
            {
                //Checks if count of clients should be updated from DB
                if (rowsCount == 0)
                {
                    rowsCount = CountOfClients();

                    if (rowsCount == 0)
                    {
                        this.ViewBag.MaxPage = (rowsCount / RowsPerPage) - (rowsCount % RowsPerPage == 0 ? 1 : 0) + 1;
                        this.ViewBag.PageNumber = pageNumber;
                        this.ViewBag.RowsCount = rowsCount;

                        return View(new List<ClientDTO>());
                    }
                }

                //Calculates maximum number of pages
                this.ViewBag.MaxPage = (rowsCount / RowsPerPage) - (rowsCount % RowsPerPage == 0 ? 1 : 0) + 1;
                this.ViewBag.PageNumber = pageNumber;
                this.ViewBag.RowsCount = rowsCount;

                if (filterOn)
                {
                    ISession session = HttpContext.Session;
                    ClientDTO FilterClient = SetFilterClientFromSearchFiltersInSession();

                    if (CommonFunctions.ArePropertiesNull(FilterClient))
                    {
                        return View(GlobalClients);
                    }

                    this.ViewBag.FilterOn = filterOn;
                    GlobalClients = _clientRepository.SearchClients(FilterClient, pageNumber, RowsPerPage);
                    this.ViewData["FilterClient"] = FilterClient;
                }
                else
                {
                    GlobalClients = _clientRepository.GetClientsForPagination(pageNumber, RowsPerPage);
                }

                return View(GlobalClients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Retreives details of client with parameter id
        /// </summary>
        /// <param name="id">ID of the client</param>
        /// <returns>Details View</returns>
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = _clientRepository.GetClientById((int)id);

                if (client == null)
                {
                    return NotFound();
                }

                return View(client);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// This action calls GetClientsByFilters service that returns records from Client table based on search filters
        /// </summary>
        /// <param name="filterClient">ClientDTO object that holds filter values</param>
        /// <returns>If the filter values in ClientDTO object are all null then it returns empty index page, otherwise it returns records from Client table based on filter values</returns>
        public IActionResult SearchClients(ClientDTO filterClient)
        {
            if (CommonFunctions.ArePropertiesNull(filterClient))
            {
                this.ViewBag.MaxPage = 1;
                this.ViewBag.PageNumber = 1;
                this.ViewBag.RowsCount = 0;

                return View("Index", GlobalClients);
            }

            GlobalClients = _clientRepository.SearchClients(filterClient, 1, RowsPerPage);

            SetSearchFiltersInSession(filterClient);

            int clientsCount = CountOfClientsWithFilters(filterClient);

            this.ViewBag.MaxPage = (clientsCount / RowsPerPage) - (clientsCount % RowsPerPage == 0 ? 1 : 0) + 1;
            this.ViewBag.PageNumber = 1;
            this.ViewBag.RowsCount = clientsCount;
            this.ViewBag.FilterOn = true;
            this.ViewData["FilterClient"] = filterClient;

            return View("Index", GlobalClients);
        }

        /// <summary>
        /// Calls create view
        /// </summary>
        /// <returns>View Create</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Calls CreateClient service that returns an ID of created Client and then it opens details page of the created client
        /// </summary>
        /// <param name="client">DTO object of a client</param>
        /// <returns>Details View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientDTO client)
        {
            try
            {
                if(!String.IsNullOrEmpty(client.DriverLicenceNumber) && IsDriverLicenceNumberTaken(client.DriverLicenceNumber))
                {
                    ModelState.AddModelError("DriverLicenceNumber", "Client with this Driver Licence Number already exists, Driver Licence Number has to be unique!");
                }

                if (ModelState.IsValid)
                {
                    int clientId = _clientRepository.CreateClient(client);
                    return RedirectToAction("Details", new { id = clientId });
                }
                return View(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls GetClientById service which returns a client based on ID and then returns a view for editing clients
        /// </summary>
        /// <param name="id">ID of the client</param>
        /// <returns>Edit View</returns>
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = _clientRepository.GetClientById((int)id);
                if (client == null)
                {
                    return NotFound();
                }
                return View(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls UpdateClient service which updates client based on the sent parameters and then redirects to details page about updated client
        /// </summary>
        /// <param name="id">ID of the client for updating</param>
        /// <param name="client">DTO object for the client with new data</param>
        /// <returns>Details Action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, ClientDTO client)
        {
            try
            {
                if (id != client.ClientId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _clientRepository.UpdateClient(client);

                    return RedirectToAction("Details", new { id = id});
                }

                return View(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Gets the client for deletion by ID and then opens page for deleting client
        /// </summary>
        /// <param name="id">ID of the client for deletion</param>
        /// <returns>Delete Vliew</returns>
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = _clientRepository.GetClientById((int)id);

                if (client == null)
                {
                    return NotFound();
                }

                return View(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls DeleteClient service that returns an ID of the deleted client as a sign of succesfull deletion
        /// </summary>
        /// <param name="id">ID of the client for deletion</param>
        /// <returns>Index Action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            try
            {
                if(id == null)
                {
                    return NotFound();
                }

                if (ClientHasReservation((int)id))
                {
                    var client = _clientRepository.GetClientById((int)id);

                    if (client == null)
                    {
                        return NotFound();
                    }

                    ViewBag.IsValid = false;
                    return View("Delete", client);
                }

                int deletedClientId = _clientRepository.DeleteClient((int)id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Sets ClientDTO object for filtering records from Client table
        /// </summary>
        /// <returns>ClientDTO object with filter values</returns>
        private ClientDTO SetFilterClientFromSearchFiltersInSession()
        {
            ISession session = HttpContext.Session;

            ClientDTO FilterClient = new ClientDTO();
            FilterClient.Firstname = session.GetString("filter_Firstname");
            FilterClient.Lastname = session.GetString("filter_Lastname");
            FilterClient.Gender = session.GetString("filter_Gender");
#pragma warning disable CS8604 // Possible null reference argument. It can't be a null reference because of the condition.
            FilterClient.Birthdate = String.IsNullOrEmpty(session.GetString("filter_Birthdate")) ? null : DateTime.Parse(session.GetString("filter_Birthdate"));
#pragma warning restore CS8604 // Possible null reference argument.
            FilterClient.DriverLicenceNumber = session.GetString("filter_DriverLicenceNumber");

            return FilterClient;
        }

        /// <summary>
        /// Sets session filter parameters
        /// </summary>
        /// <param name="filterClient">ClientDTO object that holds filter values</param>
        private void SetSearchFiltersInSession(ClientDTO filterClient)
        {
            ISession session = HttpContext.Session;
            session.SetString("filter_Firstname", String.IsNullOrEmpty(filterClient.Firstname) ? "" : filterClient.Firstname);
            session.SetString("filter_Lastname", String.IsNullOrEmpty(filterClient.Lastname) ? "" : filterClient.Lastname);
            session.SetString("filter_Gender", String.IsNullOrEmpty(filterClient.Gender) ? "" : filterClient.Gender);
            session.SetString("filter_Birthdate", filterClient.Birthdate != null ? filterClient.Birthdate.Value.ToShortDateString() : "");
            session.SetString("filter_DriverLicenceNumber", String.IsNullOrEmpty(filterClient.DriverLicenceNumber) ? "" : filterClient.DriverLicenceNumber);
        }

        /// <summary>
        /// Checks if a client with passed driverLicenceNumber already exists
        /// </summary>
        /// <param name="driverLicenceNumber">Driver ID of a client</param>
        /// <returns>true if a SearchClients returns a client, otherwise returns false</returns>
        private bool IsDriverLicenceNumberTaken(string driverLicenceNumber)
        {
            ClientDTO validationClient = new ClientDTO() { DriverLicenceNumber = driverLicenceNumber };
            IEnumerable<ClientDTO> clients = _clientRepository.SearchClients(validationClient, 1, 1);

            return clients.Count() >= 1;
        }

        /// <summary>
        /// Checks if any reservations exist with passed client id
        /// </summary>
        /// <param name="clientId">ID of client for deletion</param>
        /// <returns>true if reservation exists, otherwise false</returns>
        private bool ClientHasReservation(int clientId)
        {
            ReservationDTO validationReservation = new ReservationDTO() { ClientId = clientId };
            IEnumerable<ReservationDTO> reservations = _reservationRepository.SearchReservations(validationReservation, 1, 1);

            return reservations.Count() >= 1;
        }
    }
}
