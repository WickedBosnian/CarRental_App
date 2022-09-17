using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental_Domain.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        [Column("ReservationID")]
        public int ReservationId { get; set; }
        [Column("ClientID")]
        public int ClientId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReservationDateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReservationDateTo { get; set; }
        public bool Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; } = null!;
    }
}
