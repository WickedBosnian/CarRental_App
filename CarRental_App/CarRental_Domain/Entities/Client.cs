using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Domain.Entities
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? Birthdate { get; set; }
        public char? Gender { get; set; }
        public string DriverLicenceNumber { get; set; } = string.Empty;
        public string PersonalIDCardNumber { get; set; } = string.Empty;
    }
}
