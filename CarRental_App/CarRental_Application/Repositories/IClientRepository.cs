using CarRental_Domain.Entities;
using CarRental_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<ClientDTO> GetAllClients();
        ClientDTO GetClientById(int id);
        IEnumerable<ClientDTO> GetClientsByFilters(DateTime? birthdate, string? firstname, string? lastname, string? driverLicenceNumber, string? personalIdCardNumber, string? gender);
        int CreateClient(ClientDTO client);
        void UpdateClient(ClientDTO client);
        int DeleteClient(int id);
    }
}
