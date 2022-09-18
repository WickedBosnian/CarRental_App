using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Interfaces
{
    public interface IClientServices
    {
        Client GetClientById(int id);
        IEnumerable<Client> GetAllClients();
        IEnumerable<Client> GetClientsByFilters(DateTime? birthdate, string? firstname, string? lastname, string? driverLicenceNumber, string? personalIdCardNumber, string? gender);
        int CreateClient(Client client);
        void UpdateClient(Client client);
        int DeleteClient(int id);
    }
}
