using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.ItemImage
{
    public class NewItemImageDTO
    {
        [Required]
        public int IdItem { get; set; }

        public bool IsMain { get; set; }

        [Required]
        public string UrlImage { get; set; } = null!;
    }
}
