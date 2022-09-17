using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(int id);
        IEnumerable<Client> GetClientsByFilters();
        int CreateClient(Client client);
        void UpdateClient(Client client);
        int DeleteClient(int id);
    }
}
