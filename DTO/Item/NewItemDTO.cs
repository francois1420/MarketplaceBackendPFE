using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.Item
{
    public class NewItemDTO
    {
        [Required]
        public int IdBaseItem { get; set; }

        public string? Size { get; set; }

        public string? Color { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}
