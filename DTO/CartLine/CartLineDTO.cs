using marketplace_backend.DTO.Cart;
using marketplace_backend.DTO.Item;
using marketplace_backend.Models;

namespace marketplace_backend.DTO.CartLine
{
    public class CartLineDTO
    {
        public int IdCart { get; set; }

        public int IdItem { get; set; }

        public int Quantity { get; set; }

        public bool IsPackageSent { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ItemDTO Item { get; set; } = null!;
    }
}
