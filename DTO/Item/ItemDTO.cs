using marketplace_backend.DTO.BaseItem;
using marketplace_backend.DTO.ItemImage;
using marketplace_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.Item
{
    public class ItemDTO
    {
        public int IdItem { get; set; }

        public string? Size { get; set; }

        public string? Color { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<ItemImageDTO> ItemImages { get; set; } = new List<ItemImageDTO>();

        public virtual InsertedBaseItemDTO? BaseItem { get; set; }
    }
}
