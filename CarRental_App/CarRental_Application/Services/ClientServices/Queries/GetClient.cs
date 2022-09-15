using CarRental_Application.Interfaces.ClientInterfaces.Queries;
using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Services.ClientServices.Queries
{
    public class GetClient : IGetClient
    {
        public IEnumerable<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetClientsByFilters()
        {
            throw new NotImplementedException();
        }
    }
}
