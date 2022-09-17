using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental_Domain.Entities
{
    [Table("VehicleManufacturer")]
    public class VehicleManufacturer
    {
        [Key]
        [Column("VehicleManufacturerID")]
        public int VehicleManufacturerId { get; set; }
        [StringLength(200)]
        public string VehicleManufacturerName { get; set; } = null!;
        [StringLength(1)]
        public string? VehicleManufacturerDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
    }
}
