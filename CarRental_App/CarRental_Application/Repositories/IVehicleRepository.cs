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
        IEnumerable<VehicleDTO> GetVehiclesForPagination(int pageNumber, int rowsPerPage);
        IEnumerable<VehicleDTO> SearchVehicles(VehicleDTO vehicle, int pageNumber, int rowsPerPage);
        VehicleDTO GetVehicleById(int id);
        int CreateVehicle(VehicleDTO vehicle);
        void UpdateVehicle(VehicleDTO vehicle);
        int DeleteVehicle(int id);
        int CountOfVehicles();
        int CountOfVehiclesWithFilters(VehicleDTO filterClient);
    }
}
