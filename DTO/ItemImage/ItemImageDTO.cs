using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.ItemImage
{
    public class ItemImageDTO
    {
        public int IdImage { get; set; }

        public bool IsMain { get; set; }

        public string UrlImage { get; set; } = null!;
    }
}
