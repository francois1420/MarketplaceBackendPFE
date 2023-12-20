using marketplace_backend.DTO.BaseItem;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Services.BaseItemService
{
    public interface IBaseItemService
    {
        Task<BaseItemDTO?> GetOne(int id);
        Task<IList<BaseItemDTO>> GetAll(int? idCategory, string? nameOrDescription);
        Task<InsertedBaseItemDTO?> AddOne(NewBaseItemDTO baseItemDTO);
        Task<BaseItemDTO?> UpdateOne(int id, NewBaseItemDTO baseItemDTO);
    }
}
