using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental_UI.Data;
using CarRental_UI.Models;
using CarRental_Application.Repositories;
using CarRental_DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using CarRental_Domain.Entities;

namespace CarRental_UI.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        //private int? ClientsCount;
        private readonly int RowsPerPage = 5;
        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public int CountOfClients()
        {
            return _clientRepository.CountOfClients();
        }

        /// <summary>
        /// Index action, calls GetAllClients service which retreives all clients based on pagination
        /// </summary>
        /// <param name="pageNumber">Value of current page on pagination</param>
        /// <param name="addedNewRecord">If a new record CountOfClients is called and new count is retreived</param>
        /// <param name="clientsCount">Count of clients from DB</param>
        /// <returns>Index view of clients</returns>
        public IActionResult Index(int pageNumber = 1, bool addedNewRecord = false, int clientsCount = 0)
        {
            try
            {
                //Checks if count of clients should be updated from DB
                if (clientsCount == 0 || addedNewRecord)
                {
                    clientsCount = CountOfClients();

                    if (clientsCount == 0)
                    {
                        return View(new ClientDTO());
                    }
                }

                //Calculates maximum number of pages
                this.ViewBag.MaxPage = (clientsCount / RowsPerPage) - (clientsCount % RowsPerPage == 0 ? 1 : 0) + 1;
                this.ViewBag.PageNumber = pageNumber;
                this.ViewBag.ClientsCount = clientsCount;

                List<ClientDTO> clients = _clientRepository.GetAllClients(pageNumber, RowsPerPage).ToList();

                return View(clients);
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

        public IActionResult SearchClient(DateTime? birthdate, string? firstname, string? lastname, string? driverLicenceNumber, string? gender)
        {
            List<ClientDTO> clients = new List<ClientDTO>();


            return RedirectToAction(nameof(Index), new { clients = clients });
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
        public IActionResult Create([Bind("ClientId,Firstname,Lastname,Birthdate,Gender,DriverLicenceNumber,PersonalIdcardNumber")] ClientDTO client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int clientId = _clientRepository.CreateClient(client);
                    return Redirect($"Details/{clientId}");
                }
                return View(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Calls GetClientById service which returns a client based on ID and then returns a view for editing customer
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
        public IActionResult Edit(int? id, [Bind("ClientId,Firstname,Lastname,Birthdate,Gender,DriverLicenceNumber,PersonalIdcardNumber")] ClientDTO client)
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

                    return Redirect($"Details/{id}");
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
        /// <param name="id">ID of the clinet for deletion</param>
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

                int deletedClientId = _clientRepository.DeleteClient((int)id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "; " + ex.InnerException?.Message);
            }
        }
    }
}
