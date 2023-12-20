using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.CartRepository
{
    public class CartRepository : BaseRepositorySQL<Cart>, ICartRepository
    {
        public CartRepository(MarketplaceBackendDbContext context) : base(context) { }

        public Cart? GetActualCartByUserId(int id)
        {
            return (from Cart c in context.Set<Cart>()
                    where c.IsBought == false && c.IdUser == id
                    select c).FirstOrDefault();
        }

        public IList<Cart> GetHistoryCartsByUserId(int id)
        {
            return (from Cart c in context.Set<Cart>()
                    where c.IsBought == true && c.IdUser == id
                    select c).ToList();
        }

        public async Task<Cart?> BuyCart(Cart cart)
        {
            cart.IsBought = true;
            cart.BoughtDate = DateTime.Now;

            foreach (CartLine cl in cart.CartLines)
            {
                cl.IdItemNavigation.Stock -= cl.Quantity;
            }

            await SaveChangesAsync();

            return cart;
        }
    }
}
