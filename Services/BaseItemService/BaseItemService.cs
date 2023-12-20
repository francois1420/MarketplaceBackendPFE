using marketplace_backend.DTO;
using marketplace_backend.DTO.BaseItem;
using marketplace_backend.Models;
using marketplace_backend.Repository.BaseItemRepository;
using marketplace_backend.UnitsOfWork;

namespace marketplace_backend.Services.BaseItemService
{
    public class BaseItemService : IBaseItemService
    {
        private IBaseItemRepository _baseItemRepository;
        private ITypeConverter _typeConverter;

        public BaseItemService(ITypeConverter typeConverter) 
        {
            _baseItemRepository = new UnitOfWorkDB(new MarketplaceBackendDbContext()).BaseItemRepository;
            _typeConverter = typeConverter;
        }

        public async Task<BaseItemDTO?> GetOne(int id)
        {
            BaseItem? baseItem = await _baseItemRepository.GetByIdAsync(id);
            if (baseItem == null) return null;
            return _typeConverter.BaseItemToBaseItemDTO(baseItem);
        }

        public async Task<IList<BaseItemDTO>> GetAll(int? idCategory, string? nameOrDescription)
        {
            return (await _baseItemRepository
            .SearchForAsync(baseItem =>
                (idCategory == null || baseItem.IdCategory == idCategory)
                && (nameOrDescription == null || baseItem.Name.ToLower().Contains(nameOrDescription.ToLower())
                || baseItem.Description.ToLower().Contains(nameOrDescription.ToLower()))))
            .Select(baseItem => _typeConverter.BaseItemToBaseItemDTO(baseItem))
            .ToList();
        }

        public async Task<InsertedBaseItemDTO?> AddOne(NewBaseItemDTO baseItemDTO)
        {
            BaseItem baseItem = _typeConverter.BaseItemDTOToBaseItem(baseItemDTO);
            BaseItem? insertedBaseItem = await _baseItemRepository.InsertAsync(baseItem);
            if (insertedBaseItem == null) return null;
            InsertedBaseItemDTO insertedBaseItemDTO = _typeConverter.BaseItemToInsertedBaseItemDTO(insertedBaseItem);
            return insertedBaseItemDTO;
        }

        public async Task<BaseItemDTO?> UpdateOne(int id, NewBaseItemDTO baseItemDTO)
        {
            BaseItem? updatedBaseItem = await _baseItemRepository.UpdateOne(id, baseItemDTO);
            if (updatedBaseItem == null) return null;
            BaseItemDTO updatedBaseItemDTO = _typeConverter.BaseItemToBaseItemDTO(updatedBaseItem);
            return updatedBaseItemDTO;
        }
    }
}
