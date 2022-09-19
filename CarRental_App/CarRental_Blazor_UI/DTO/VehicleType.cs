using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRental_Blazor_UI.DTO
{
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        [StringLength(200)]
        public string? VehicleTypeName { get; set; }
        public string? VehicleTypeDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
