using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental_Domain.Entities
{
    [Table("Vehicle")]
    public class Vehicle
    {
        [Key]
        [Column("VehicleID")]
        public int VehicleId { get; set; }
        [Column("VehicleName")]
        public string? VehicleName { get; set; }
        [Column("VehicleManufacturerID")]
        public int? VehicleManufacturerId { get; set; }
        [Column("VehicleTypeID")]
        public int? VehicleTypeId { get; set; }
        [StringLength(20)]
        public string? Color { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateManufactured { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal? PricePerDay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("VehicleManufacturerId")]
        public virtual VehicleManufacturer? VehicleManufacturer { get; set; }
        [ForeignKey("VehicleTypeId")]
        public virtual VehicleType? VehicleType { get; set; }
    }
}
