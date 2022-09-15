using CarRental_Application.Interfaces.SharedInterfaces.Commands;
using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Services.ClientServices.Commands
{
    public class DeleteClient : IDelete<Client>
    {
        public int Delete(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
