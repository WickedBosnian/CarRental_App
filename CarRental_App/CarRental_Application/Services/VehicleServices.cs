using CarRental_Application.Interfaces;
using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Application.Services
{
    public class VehicleServices : IVehicleServices
    {
        private IVehicleRepository _vehicleRepository;
        public VehicleServices(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public int CreateVehicle(Vehicle vehicle)
        {
            return _vehicleRepository.CreateVehicle(vehicle);
        }

        public int DeleteVehicle(int id)
        {
            return _vehicleRepository.DeleteVehicle(id);
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return _vehicleRepository.GetAllVehicles();
        }

        public Vehicle GetVehicleById(int id)
        {
            return _vehicleRepository.GetVehicleById(id);
        }

        public IEnumerable<Vehicle> SearchVehicles(string? vehicleName, int? vehicleManufacturerId, int? vehicleTypeId)
        {
            return _vehicleRepository.SearchVehicles(vehicleName, vehicleManufacturerId, vehicleTypeId);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _vehicleRepository.UpdateVehicle(vehicle);
        }
    }
}
