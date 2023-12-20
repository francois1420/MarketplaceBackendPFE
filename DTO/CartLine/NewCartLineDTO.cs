using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.CartLine
{
    public class NewCartLineDTO
    {
        [Required]
        public int IdCart { get; set; }

        [Required]
        public int IdItem { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
