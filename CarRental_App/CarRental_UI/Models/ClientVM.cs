using CarRental_DTO;
using System.ComponentModel.DataAnnotations;

namespace CarRental_UI.Models
{
    public class ClientVM
    {
        [Key]
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

        public static explicit operator ClientVM(List<ClientDTO> v)
        {
            throw new NotImplementedException();
        }
    }
}
