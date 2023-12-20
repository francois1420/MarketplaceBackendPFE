using marketplace_backend.DTO.Item;

namespace marketplace_backend.Services.ItemService
{
    public interface IItemService
    {
        Task<ItemDTO?> AddOne(NewItemDTO newItemDTO);
        Task<ItemDTO?> UpdateOne(int id, UpdatedItemDTO updatedItemDTO);
    }
}
