using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.Category
{
    public class CategoryDTO
    {
        public int IdCategory { get; set; }

        public string Name { get; set; } = null!;

        public string UrlImage { get; set; } = null!;
    }
}
