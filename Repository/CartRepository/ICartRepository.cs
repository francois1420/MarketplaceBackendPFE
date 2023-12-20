using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.CartRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart? GetActualCartByUserId(int id);
        IList<Cart> GetHistoryCartsByUserId(int id);

        Task<Cart?> BuyCart(Cart cart);
    }
}
