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
    public class ClientDTO
    {
        [Key]
        public int? ClientId { get; set; }
        [StringLength(200), Required]
        public string? Firstname { get; set; }
        [StringLength(200), Required]
        public string? Lastname { get; set; }
        public DateTime? Birthdate { get; set; }
        [StringLength(1)]
        public string? Gender { get; set; }
        [StringLength(20), DisplayName("Driver Licence Number"), Required]
        public string? DriverLicenceNumber { get; set; }
        [StringLength(20), DisplayName("Personal ID card Number")]
        public string? PersonalIdcardNumber { get; set; }
    }
}
