using CarRental_Domain.Entities;
using CarRental_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Repositories
{
    public interface IVehicleTypeRepository
    {
        IEnumerable<VehicleTypeDTO> GetAllVehicleTypes();
        VehicleTypeDTO GetVehicleTypeById(int id);
        int CreateVehicleType(VehicleTypeDTO vehicleType);
        int DeleteVehicleType(int id);
        void UpdateVehicleType(VehicleTypeDTO vehicleType);
    }
}
