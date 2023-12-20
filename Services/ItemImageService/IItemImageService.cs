using marketplace_backend.DTO.Item;
using marketplace_backend.DTO.ItemImage;

namespace marketplace_backend.Services.ItemImageService
{
    public interface IItemImageService
    {
        Task<ItemImageDTO?> AddOne(NewItemImageDTO newItemImageDTO);
        Task<ItemImageDTO?> UpdateOne(int idItemImage, UpdatedItemImageDTO updatedItemImageDTO);
        void DeleteOne(int id);
    }
}
