using marketplace_backend.DTO;
using marketplace_backend.DTO.Item;
using marketplace_backend.Models;
using marketplace_backend.Repository.ItemRepository;
using marketplace_backend.UnitsOfWork;

namespace marketplace_backend.Services.ItemService
{
    public class ItemService : IItemService
    {
        private IItemRepository _itemRepository;
        private ITypeConverter _typeConverter;

        public ItemService(ITypeConverter typeConverter)
        {
            _itemRepository = new UnitOfWorkDB(new MarketplaceBackendDbContext()).ItemRepository;
            _typeConverter = typeConverter;
        }

        public async Task<ItemDTO?> AddOne(NewItemDTO newItemDTO)
        {
            Item? insertedItem = await _itemRepository.InsertAsync(_typeConverter.ItemDTOToItem(newItemDTO));
            if (insertedItem == null) return null;
            ItemDTO itemDTO = _typeConverter.ItemToItemDTO(insertedItem);
            return itemDTO;
        }

        public async Task<ItemDTO?> UpdateOne(int id, UpdatedItemDTO updatedItemDTO)
        {
            Item? item = await _itemRepository.UpdateOne(id, updatedItemDTO);
            if (item == null) return null;
            ItemDTO itemDTO = _typeConverter.ItemToItemDTO(item);
            return itemDTO;
        }
    }
}
