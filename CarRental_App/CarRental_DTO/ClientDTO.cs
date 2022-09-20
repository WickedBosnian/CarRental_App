using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_DTO
{
    public class ClientDTO
    {
        public int? ClientId { get; set; }
        [StringLength(200)]
        public string? Firstname { get; set; }
        [StringLength(200)]
        public string? Lastname { get; set; }
        public DateTime? Birthdate { get; set; }
        [StringLength(1)]
        public string? Gender { get; set; }
        [StringLength(20)]
        public string? DriverLicenceNumber { get; set; }
        [StringLength(20)]
        public string? PersonalIdcardNumber { get; set; }
    }
}
