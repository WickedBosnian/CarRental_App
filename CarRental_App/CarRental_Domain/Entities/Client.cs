using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Domain.Entities
{
    [Table("Client")]
    public class Client
    {
        [Key]
        [Column("ClientID")]
        public int ClientId { get; set; }
        [StringLength(200)]
        public string Firstname { get; set; } = null!;
        [StringLength(200)]
        public string Lastname { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? Birthdate { get; set; }
        [StringLength(1)]
        public string? Gender { get; set; }
        [StringLength(20)]
        public string DriverLicenceNumber { get; set; } = null!;
        [Column("PersonalIDCardNumber")]
        [StringLength(20)]
        public string? PersonalIdcardNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
    }
}
