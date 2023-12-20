namespace marketplace_backend.DTO.Address
{
    public class AddressDTO
    {
        public int IdAddress { get; set; }

        public string StreetNumber { get; set; } = null!;

        public string? AppartmentNumber { get; set; }

        public string StreetName { get; set; } = null!;

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public string Country { get; set; } = null!;
    }
}
