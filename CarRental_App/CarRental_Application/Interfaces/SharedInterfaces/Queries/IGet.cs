using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Interfaces.SharedInterfaces.Queries
{
    public interface IGet<T>
    {
        public T GetById(int id);
        public IEnumerable<T> GetAll();
    }
}
