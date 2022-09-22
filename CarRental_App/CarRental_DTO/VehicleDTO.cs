using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CarRental_DTO
{
    public class VehicleDTO
    {
        [Key]
        public int? VehicleId { get; set; }
        [StringLength(200), Required, DisplayName("Vehicle Name")]
        public string? VehicleName { get; set; }
        [DisplayName("Vehicle Manufacturer"), Required]
        public int? VehicleManufacturerId { get; set; }
        [DisplayName("Vehicle Type"), Required]
        public int? VehicleTypeId { get; set; }
        [StringLength(20)]
        public string? Color { get; set; }
        [DisplayName("Date Manufactured"), Required]
        public DateTime? DateManufactured { get; set; }
        [DisplayName("Price Per Day"), Required, RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public decimal? PricePerDay { get; set; }
        public virtual VehicleManufacturerDTO? VehicleManufacturer { get; set; }
        public virtual VehicleTypeDTO? VehicleType { get; set; }
    }
}
