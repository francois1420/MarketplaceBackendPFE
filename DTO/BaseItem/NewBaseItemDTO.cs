using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.BaseItem
{
    public class NewBaseItemDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public int IdCategory { get; set; }
    }
}
