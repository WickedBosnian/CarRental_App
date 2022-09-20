using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_DTO
{
    public class VehicleDTO
    {
        public int? VehicleId { get; set; }
        [StringLength(200)]
        public string? VehicleName { get; set; }
        public int? VehicleManufacturerId { get; set; }
        public int? VehicleTypeId { get; set; }
        [StringLength(20)]
        public string? Color { get; set; }
        public DateTime? DateManufactured { get; set; }
        public decimal? PricePerDay { get; set; }
        public virtual VehicleManufacturerDTO? VehicleManufacturer { get; set; }
        public virtual VehicleTypeDTO? VehicleType { get; set; }
    }
}
