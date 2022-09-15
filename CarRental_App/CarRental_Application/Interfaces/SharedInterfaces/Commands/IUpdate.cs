using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Interfaces.SharedInterfaces.Commands
{
    public interface IUpdate<T>
    {
        void Update(T entity);
    }
}
