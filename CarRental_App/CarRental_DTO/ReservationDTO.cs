using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_DTO
{
    public class ReservationDTO
    {
        public int? ReservationId { get; set; }
        public int? ClientId { get; set; }
        public int? VehicleID { get; set; }
        public DateTime? ReservationDateFrom { get; set; }
        public DateTime? ReservationDateTo { get; set; }
        public bool? Active { get; set; }
        public virtual ClientDTO? Client { get; set; }
        public virtual VehicleDTO? Vehicle { get; set; }
    }
}
