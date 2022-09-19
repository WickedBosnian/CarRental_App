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
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Gender { get; set; }
        public string? DriverLicenceNumber { get; set; }
        public string? PersonalIdcardNumber { get; set; }
    }
}
