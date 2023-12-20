using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.Address
{
    public class NewAddressDTO
    {
        [Required]
        public string StreetNumber { get; set; } = null!;

        public string? AppartmentNumber { get; set; }

        [Required]
        public string StreetName { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;

        [Required]
        public string ZipCode { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;
    }
}
