using marketplace_backend.DTO.CartLine;
using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.CartLineRepository
{
    public class CartLineRepository : BaseRepositorySQL<CartLine>, ICartLineRepository
    {
        public CartLineRepository(MarketplaceBackendDbContext context) : base(context) { }

        public async Task<CartLine?> UpdateOne(int idCart, int idItem, int quantity)
        {
            CartLine? cartLine = (from CartLine cl in context.Set<CartLine>()
                                  where cl.IdCart == idCart && cl.IdItem == idItem
                                  select cl).FirstOrDefault();

            if (cartLine == null) return null;

            cartLine.Quantity = quantity;

            await SaveChangesAsync();

            return cartLine;
        }

        public async Task<CartLine?> UpdateOneItemSizeOrColor(int idCart, int idItem, string size, string color)
        {
            CartLine? cartLine = (from CartLine cl in context.Set<CartLine>()
                                  where cl.IdCart == idCart && cl.IdItem == idItem
                                  select cl).FirstOrDefault();

            if (cartLine == null) return null;

            cartLine.IdItemNavigation.Size = size;
            cartLine.IdItemNavigation.Color = color;

            await SaveChangesAsync();

            return cartLine;
        }
        public async Task<CartLine?> GetCartLine(int idCart, int idItem)
        {
            CartLine? cartLine = (from CartLine cl in context.Set<CartLine>()
                                  where cl.IdCart == idCart && cl.IdItem == idItem
                                  select cl).FirstOrDefault();

            if (cartLine == null) return null;

            return cartLine;
        }
    }
}
