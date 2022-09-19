using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Repositories
{
    public interface IVehicleTypeRepository
    {
        IEnumerable<VehicleType> GetAllVehicleTypes();
        VehicleType GetVehicleTypeById(int id);
        int CreateVehicleType(VehicleType vehicleType);
        int DeleteVehicleType(int id);
        void UpdateVehicleType(VehicleType vehicleType);
    }
}
