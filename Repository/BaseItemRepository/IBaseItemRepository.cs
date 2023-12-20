using marketplace_backend.DTO.BaseItem;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.BaseItemRepository
{
    public interface IBaseItemRepository : IRepository<BaseItem>
    {
        Task<BaseItem?> UpdateOne(int idBaseItem, NewBaseItemDTO newBaseItemDTO);
    }
}
