using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_DTO
{
    public class VehicleManufacturerDTO
    {
        [Key]
        public int? VehicleManufacturerId { get; set; }
        [StringLength(200), Required, DisplayName("Vehicle Manufacturer Name")]
        public string? VehicleManufacturerName { get; set; }
        [DisplayName("Vehicle Manufacturer Description")]
        public string? VehicleManufacturerDescription { get; set; }
    }
}
