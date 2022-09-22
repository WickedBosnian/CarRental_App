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
    public class VehicleTypeDTO
    {
        [Key]
        public int? VehicleTypeId { get; set; }
        [StringLength(200), Required]
        [DisplayName("Vehicle Type Name")]
        public string? VehicleTypeName { get; set; }
        [DisplayName("Vehicle Type Description")]
        public string? VehicleTypeDescription { get; set; }
    }
}
