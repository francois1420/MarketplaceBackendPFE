using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.ItemImage
{
    public class UpdatedItemImageDTO
    {
        [Required]
        public bool IsMain { get; set; }

        [Required]
        public string UrlImage { get; set; } = null!;
    }
}
