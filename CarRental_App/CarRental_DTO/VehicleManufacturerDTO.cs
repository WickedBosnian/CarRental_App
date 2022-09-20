using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_DTO
{
    public class VehicleManufacturerDTO
    {
        public int? VehicleManufacturerId { get; set; }
        [StringLength(200)]
        public string? VehicleManufacturerName { get; set; }
        public string? VehicleManufacturerDescription { get; set; }
    }
}
