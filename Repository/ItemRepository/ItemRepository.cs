using marketplace_backend.DTO.Item;
using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.ItemRepository
{
    public class ItemRepository : BaseRepositorySQL<Item>, IItemRepository
    {
        public ItemRepository(MarketplaceBackendDbContext context) : base(context) { }

        public async Task<Item?> UpdateOne(int idItem, UpdatedItemDTO updatedItemDTO)
        {
            Item? item = (from Item i in context.Set<Item>()
                          where i.IdItem == idItem
                          select i).FirstOrDefault();

            if (item == null) return null;

            item.Price = updatedItemDTO.Price;
            item.Stock = updatedItemDTO.Stock;
            if (updatedItemDTO.Size != null)
                item.Size = updatedItemDTO.Size;
            if (updatedItemDTO.Color != null)
                item.Color = updatedItemDTO.Color;

            await SaveChangesAsync();

            return item;
        }
    }
}
