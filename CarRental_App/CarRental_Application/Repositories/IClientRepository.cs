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
        IEnumerable<ClientDTO> GetClientsForPagination(int pageNumber, int rowsPerPage);
        ClientDTO GetClientById(int id);
        IEnumerable<ClientDTO> SearchClients(ClientDTO client, int pageNumber, int rowsPerPage);
        int CreateClient(ClientDTO client);
        void UpdateClient(ClientDTO client);
        int DeleteClient(int id);
        int CountOfClients();
        int CountOfClientsWithFilters(ClientDTO filterClient);
    }
}
