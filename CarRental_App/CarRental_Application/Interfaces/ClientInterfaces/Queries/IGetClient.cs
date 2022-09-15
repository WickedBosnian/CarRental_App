using CarRental_Application.Interfaces.SharedInterfaces.Queries;
using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Interfaces.ClientInterfaces.Queries
{
    public interface IGetClient : IGet<Client>
    {
        public IEnumerable<Client> GetClientsByFilters();
    }
}
