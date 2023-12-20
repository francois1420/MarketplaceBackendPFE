using marketplace_backend.DTO.CartLine;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.CartLineRepository
{
    public interface ICartLineRepository : IRepository<CartLine>
    {
        Task<CartLine?> UpdateOne(int idCart, int idItem, int quantity);
        Task<CartLine?> UpdateOneItemSizeOrColor(int idCart, int idItem, string size, string color);
        Task<CartLine?> GetCartLine(int idCart, int idItem);
    }
}
