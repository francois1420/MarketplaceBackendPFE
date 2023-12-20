using marketplace_backend.DTO;
using marketplace_backend.DTO.ItemImage;
using marketplace_backend.Models;
using marketplace_backend.Repository.ItemImageRepository;
using marketplace_backend.UnitsOfWork;

namespace marketplace_backend.Services.ItemImageService
{
    public class ItemImageService : IItemImageService
    {
        private IItemImageRepository _itemImageRepository;
        private ITypeConverter _typeConverter;

        public ItemImageService(ITypeConverter typeConverter)
        {
            _itemImageRepository = new UnitOfWorkDB(new MarketplaceBackendDbContext()).ItemImageRepository;
            _typeConverter = typeConverter;
        }

        public async Task<ItemImageDTO?> AddOne(NewItemImageDTO newItemImageDTO)
        {
            ItemImage itemImage = _typeConverter.ItemImageDTOToItemImage(newItemImageDTO);
            ItemImage? insertedItemImage = await _itemImageRepository.InsertAsync(itemImage);
            if (insertedItemImage == null) return null;
            ItemImageDTO itemImageDTO = _typeConverter.ItemImageToItemImageDTO(insertedItemImage);
            return itemImageDTO;
        }

        public async Task<ItemImageDTO?> UpdateOne(int idItemImage, UpdatedItemImageDTO updatedItemImageDTO)
        {
            ItemImage? updatedItemImage = await _itemImageRepository.UpdateOne(idItemImage, updatedItemImageDTO);
            if (updatedItemImage == null) return null;
            ItemImageDTO itemImageDTO = _typeConverter.ItemImageToItemImageDTO(updatedItemImage);
            return itemImageDTO;
        }

        public async void DeleteOne(int idItemImage)
        {
            await _itemImageRepository.DeleteAsync(new ItemImage() { IdImage = idItemImage });
        }
    }
}
