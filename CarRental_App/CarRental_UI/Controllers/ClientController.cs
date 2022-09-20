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

namespace CarRental_UI.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }


        // GET: Client
        public IActionResult Index()
        {
              return View(_clientRepository.GetAllClients());
        }

        // GET: Client/Details/5
        public IActionResult Details(int? id)
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

        //// GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ClientId,Firstname,Lastname,Birthdate,Gender,DriverLicenceNumber,PersonalIdcardNumber")] ClientDTO client)
        {
            if (ModelState.IsValid)
            {
                int clientId = _clientRepository.CreateClient(client);
                return Redirect($"Details/{clientId}");
            }
            return View(client);
        }

        // GET: Client/Edit/5
        public IActionResult Edit(int? id)
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

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("ClientId,Firstname,Lastname,Birthdate,Gender,DriverLicenceNumber,PersonalIdcardNumber")] ClientDTO client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _clientRepository.UpdateClient(client);

                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        // GET: Client/Delete/5
        public IActionResult Delete(int? id)
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

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            int deletedClientId = _clientRepository.DeleteClient((int)id);

            return RedirectToAction(nameof(Index));
        }
    }
}
