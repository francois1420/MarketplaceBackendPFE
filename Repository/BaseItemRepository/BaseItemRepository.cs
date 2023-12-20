using marketplace_backend.DTO.BaseItem;
using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.BaseItemRepository
{
    public class BaseItemRepository : BaseRepositorySQL<BaseItem>, IBaseItemRepository
    {
        public BaseItemRepository(MarketplaceBackendDbContext context) : base(context) { }

        public async Task<BaseItem?> UpdateOne(int idBaseItem, NewBaseItemDTO newBaseItemDTO)
        {
            BaseItem? baseItem = (from BaseItem b in context.Set<BaseItem>()
                          where b.IdBaseItem == idBaseItem
                          select b).FirstOrDefault();

            if (baseItem == null) return null;

            baseItem.Name = newBaseItemDTO.Name;
            baseItem.Description = newBaseItemDTO.Description;
            baseItem.IdCategory = newBaseItemDTO.IdCategory;

            await SaveChangesAsync();

            return baseItem;
        }
    }
}
