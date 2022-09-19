using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Interfaces
{
    public interface IVehicleManufacturerServices
    {
        IEnumerable<VehicleManufacturer> GetAllVehicleManufacturers();
        VehicleManufacturer GetVehicleManufacturerById(int id);
        int DeleteVehicleManufacturer(int id);
        int CreateVehicleManufacturer(VehicleManufacturer vehicleManufacturer);
        void UpdateVehicleManufacturer(VehicleManufacturer vehicleManufacturer);
    }
}
