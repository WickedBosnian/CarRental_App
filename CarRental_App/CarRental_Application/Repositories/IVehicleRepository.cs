using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Repositories
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> GetAllVehicles();
        IEnumerable<Vehicle> SearchVehicles(string? vehicleName, int? vehicleManufacturerId, int? vehicleTypeId);
        Vehicle GetVehicleById(int id);
        int CreateVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        int DeleteVehicle(int id);
    }
}
