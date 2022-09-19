using CarRental_Domain.Entities;
using CarRental_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Repositories
{
    public interface IVehicleRepository
    {
        IEnumerable<VehicleDTO> GetAllVehicles();
        IEnumerable<VehicleDTO> SearchVehicles(string? vehicleName, int? vehicleManufacturerId, int? vehicleTypeId);
        VehicleDTO GetVehicleById(int id);
        int CreateVehicle(VehicleDTO vehicle);
        void UpdateVehicle(VehicleDTO vehicle);
        int DeleteVehicle(int id);
    }
}
