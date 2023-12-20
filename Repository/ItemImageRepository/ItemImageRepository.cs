using marketplace_backend.DTO.User;
using marketplace_backend.DTO.ItemImage;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.ItemImageRepository
{
    public class ItemImageRepository : BaseRepositorySQL<ItemImage>, IItemImageRepository
    {
        public ItemImageRepository(MarketplaceBackendDbContext context) : base(context) { }

        public async Task<ItemImage?> UpdateOne(int idImage, UpdatedItemImageDTO updatedItemImageDTO)
        {
            ItemImage? itemImage = (from ItemImage i in context.Set<ItemImage>()
                          where i.IdImage == idImage
                          select i).FirstOrDefault();

            if (itemImage == null) return null;

            itemImage.IsMain = updatedItemImageDTO.IsMain;
            itemImage.UrlImage = updatedItemImageDTO.UrlImage;

            await SaveChangesAsync();

            return itemImage;
        }

        public async void DeleteAllByItemId(int idItem)
        {
            IQueryable<ItemImage> itemImages = (from ItemImage i in context.Set<ItemImage>()
                                              where i.IdItem == idItem
                                              select i);

            foreach (ItemImage itemImage in itemImages)
            {
                context.Remove(itemImage);
            }

            await SaveChangesAsync();
        }
    }
}
