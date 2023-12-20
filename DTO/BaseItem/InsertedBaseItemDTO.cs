namespace marketplace_backend.DTO.BaseItem
{
    public class InsertedBaseItemDTO
    {
        public int IdBaseItem { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public int IdCategory { get; set; }
    }
}
