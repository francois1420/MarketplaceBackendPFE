
using marketplace_backend.DTO.Address;
using marketplace_backend.DTO.BaseItem;
using marketplace_backend.DTO.Cart;
using marketplace_backend.DTO.CartLine;
using marketplace_backend.DTO.Category;
using marketplace_backend.DTO.Item;
using marketplace_backend.DTO.ItemImage;
using marketplace_backend.DTO.User;
using marketplace_backend.Models;

namespace marketplace_backend.DTO
{
    public interface ITypeConverter
    {
        Models.BaseItem BaseItemDTOToBaseItem(NewBaseItemDTO baseItemDTO);
        BaseItemDTO BaseItemToBaseItemDTO(Models.BaseItem baseItem);
        InsertedBaseItemDTO? BaseItemToInsertedBaseItemDTO(Models.BaseItem baseItem);

        Models.CartLine CartLineDTOToCartLine(NewCartLineDTO newCartLineDTO);
        CartLineDTO CartLineToCartLineDTO(Models.CartLine cartLine);

        CartDTO CartToCartDTO(Models.Cart cart);

        Models.Category CategoryDTOToCategory(CategoryDTO categoryDTO);
        CategoryDTO CategoryToCategoryDTO(Models.Category category);
        Models.Category NewCategoryDTOToCategory(NewCategoryDTO newCategoryDTO);

        Models.ItemImage ItemImageDTOToItemImage(NewItemImageDTO newItemImageDTO);
        ItemImageDTO ItemImageToItemImageDTO(Models.ItemImage itemImage);

        Models.Item ItemDTOToItem(NewItemDTO newItemDTO);
        ItemDTO ItemToItemDTO(Models.Item item);

        Models.User UserDTOToUser(NewUserDTO user);
        UserDTO UserToUserDTO(Models.User user);

        Models.Address NewAddressDTOToAddress(NewAddressDTO newAddressDTO);
        AddressDTO AddressToAddressDTO(Models.Address address);
    }
}
