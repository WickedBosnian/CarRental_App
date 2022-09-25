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
        public int? VehicleId { get; set; }
        [Required, DisplayName("Reservation From"), DataType(DataType.Date)]
        public DateTime? ReservationDateFrom { get; set; }
        [Required, DisplayName("Reservation To"), DataType(DataType.Date)]
        public DateTime? ReservationDateTo { get; set; }
        public bool? Active { get; set; }
        [DisplayName("Client - Driver ID")]
        public virtual ClientDTO? Client { get; set; }
        public virtual VehicleDTO? Vehicle { get; set; }
    }
}
