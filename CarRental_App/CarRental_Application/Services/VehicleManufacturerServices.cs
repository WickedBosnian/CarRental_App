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
    public class VehicleManufacturerServices : IVehicleManufacturerServices
    {
        private IVehicleManufacturerRepository _vehicleManufacturerRepository;
        public VehicleManufacturerServices(IVehicleManufacturerRepository vehicleManufacturerRepository)
        {
            _vehicleManufacturerRepository = vehicleManufacturerRepository;
        }

        public int CreateVehicleManufacturer(VehicleManufacturer vehicleManufacturer)
        {
            return _vehicleManufacturerRepository.CreateVehicleManufacturer(vehicleManufacturer);
        }

        public int DeleteVehicleManufacturer(int id)
        {
            return _vehicleManufacturerRepository.DeleteVehicleManufacturer(id);
        }

        public IEnumerable<VehicleManufacturer> GetAllVehicleManufacturers()
        {
            return _vehicleManufacturerRepository.GetAllVehicleManufacturers();
        }

        public VehicleManufacturer GetVehicleManufacturerById(int id)
        {
            return _vehicleManufacturerRepository.GetVehicleManufacturerById(id);
        }

        public void UpdateVehicleManufacturer(VehicleManufacturer vehicleManufacturer)
        {
            _vehicleManufacturerRepository.UpdateVehicleManufacturer(vehicleManufacturer);
        }
    }
}
