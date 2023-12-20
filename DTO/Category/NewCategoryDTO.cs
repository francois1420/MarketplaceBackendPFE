using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.Category
{
    public class NewCategoryDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string UrlImage { get; set; } = null!;
    }
}
