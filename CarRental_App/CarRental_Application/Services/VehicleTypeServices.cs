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
    public class VehicleTypeServices : IVehicleTypeServices
    {
        private IVehicleTypeRepository _vehicleTypeRepository;

        public VehicleTypeServices(IVehicleTypeRepository vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }
        public int CreateVehicleType(VehicleType vehicleType)
        {
            return _vehicleTypeRepository.CreateVehicleType(vehicleType);
        }

        public int DeleteVehicleType(int id)
        {
            return _vehicleTypeRepository.DeleteVehicleType(id);
        }

        public IEnumerable<VehicleType> GetAllVehicleTypes()
        {
            return _vehicleTypeRepository.GetAllVehicleTypes();
        }

        public VehicleType GetVehicleTypeById(int id)
        {
            return _vehicleTypeRepository.GetVehicleTypeById(id);
        }

        public void UpdateVehicleType(VehicleType vehicleType)
        {
            _vehicleTypeRepository.UpdateVehicleType(vehicleType);
        }
    }
}
