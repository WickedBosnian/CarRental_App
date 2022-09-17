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
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clientRepository.GetAllClients();
        }

        public Client GetClientById(int id)
        {
            return _clientRepository.GetClientById(id);
        }

        public IEnumerable<Client> GetClientsByFilters()
        {
            return _clientRepository.GetClientsByFilters();
        }

        public void UpdateClient(Client client)
        {
            _clientRepository.UpdateClient(client);
        }
    }
}
