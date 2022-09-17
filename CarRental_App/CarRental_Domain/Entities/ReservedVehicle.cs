using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental_Domain.Entities
{
    [Table("ReservedVehicle")]
    public class ReservedVehicle
    {
        [Key]
        [Column("ReservedVehicleID")]
        public int ReservedVehicleId { get; set; }
        [Column("ReservationID")]
        public int ReservationId { get; set; }
        [Column("VehicleID")]
        public int VehicleId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("ReservationId")]
        public virtual Reservation Reservation { get; set; } = null!;
        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
