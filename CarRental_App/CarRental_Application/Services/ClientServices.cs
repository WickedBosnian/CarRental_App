using CarRental_Application.Interfaces;
using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Services
{
    public class ClientServices : IClientServices
    {
        private IClientRepository _clientRepository;

        public ClientServices(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public int CreateClient(Client client)
        {
            return _clientRepository.CreateClient(client);
        }

        public int DeleteClient(int id)
        {
            return _clientRepository.DeleteClient(id);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clientRepository.GetAllClients();
        }

        public Client GetClientById(int id)
        {
            return _clientRepository.GetClientById(id);
        }

        public IEnumerable<Client> GetClientsByFilters(DateTime? birthdate, string? firstname, string? lastname, string? driverLicenceNumber, string? personalIdCardNumber, string? gender)
        {
            return _clientRepository.GetClientsByFilters(birthdate, firstname, lastname, driverLicenceNumber, personalIdCardNumber, gender);
        }

        public void UpdateClient(Client client)
        {
            _clientRepository.UpdateClient(client);
        }
    }
}
