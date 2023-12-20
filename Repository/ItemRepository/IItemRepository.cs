using marketplace_backend.DTO.Item;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.ItemRepository
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<Item?> UpdateOne(int idItem, UpdatedItemDTO updatedItemDTO);
    }
}
