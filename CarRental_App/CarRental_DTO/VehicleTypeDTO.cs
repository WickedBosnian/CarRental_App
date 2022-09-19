using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_DTO
{
    public class VehicleTypeDTO
    {
        public int? VehicleTypeId { get; set; }
        public string? VehicleTypeName { get; set; }
        public string? VehicleTypeDescription { get; set; }
    }
}
