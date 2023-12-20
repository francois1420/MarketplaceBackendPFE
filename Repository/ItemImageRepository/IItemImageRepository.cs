using marketplace_backend.DTO.ItemImage;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.ItemImageRepository
{
    public interface IItemImageRepository : IRepository<ItemImage>
    {
        Task<ItemImage?> UpdateOne(int idImage, UpdatedItemImageDTO updatedItemImageDTO);
    }
}
