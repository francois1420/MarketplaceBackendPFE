using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.Item
{
    public class UpdatedItemDTO
    {
        public string? Size { get; set; }

        public string? Color { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
