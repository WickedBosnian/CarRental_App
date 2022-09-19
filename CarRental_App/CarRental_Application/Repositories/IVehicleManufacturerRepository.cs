using CarRental_Domain.Entities;
using CarRental_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Repositories
{
    public interface IVehicleManufacturerRepository
    {
        IEnumerable<VehicleManufacturerDTO> GetAllVehicleManufacturers();
        VehicleManufacturerDTO GetVehicleManufacturerById(int id);
        int DeleteVehicleManufacturer(int id);
        int CreateVehicleManufacturer(VehicleManufacturerDTO vehicleManufacturer);
        void UpdateVehicleManufacturer(VehicleManufacturerDTO vehicleManufacturer);
    }
}
