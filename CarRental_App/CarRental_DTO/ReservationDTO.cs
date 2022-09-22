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
    public class ReservationDTO
    {
        [Key]
        public int? ReservationId { get; set; }
        [Required, DisplayName("Client")]
        public int? ClientId { get; set; }
        [Required, DisplayName("Vehicle")]
        public int? VehicleID { get; set; }
        [Required, DisplayName("Reservation From")]
        public DateTime? ReservationDateFrom { get; set; }
        [Required, DisplayName("Reservation To")]
        public DateTime? ReservationDateTo { get; set; }
        public bool? Active { get; set; }
        public virtual ClientDTO? Client { get; set; }
        public virtual VehicleDTO? Vehicle { get; set; }
    }
}
