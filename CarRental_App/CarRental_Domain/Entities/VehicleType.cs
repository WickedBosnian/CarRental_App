using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental_Domain.Entities
{
    [Table("VehicleType")]
    public class VehicleType
    {
        [Key]
        [Column("VehicleTypeID")]
        public int VehicleTypeId { get; set; }
        [StringLength(200)]
        public string VehicleTypeName { get; set; } = null!;
        public string? VehicleTypeDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
    }
}
