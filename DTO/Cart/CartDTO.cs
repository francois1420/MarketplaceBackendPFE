using marketplace_backend.DTO.CartLine;
using marketplace_backend.Models;

namespace marketplace_backend.DTO.Cart
{
    public class CartDTO
    {
        public int IdCart { get; set; }

        public int IdUser { get; set; }

        public bool IsBought { get; set; }

        public DateTime? BoughtDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<CartLineDTO> CartLines { get; set; } = new List<CartLineDTO>();
    }
}
