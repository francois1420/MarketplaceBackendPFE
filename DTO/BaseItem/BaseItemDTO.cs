using marketplace_backend.DTO.Item;

namespace marketplace_backend.DTO.BaseItem
{
    public class BaseItemDTO
    {
        public int IdBaseItem { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public int IdCategory { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<ItemDTO> Items { get; set; } = new List<ItemDTO>();

    }
}
