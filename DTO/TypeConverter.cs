

using marketplace_backend.DTO.Address;
using marketplace_backend.DTO.BaseItem;
using marketplace_backend.DTO.Cart;
using marketplace_backend.DTO.CartLine;
using marketplace_backend.DTO.Category;
using marketplace_backend.DTO.Item;
using marketplace_backend.DTO.ItemImage;
using marketplace_backend.DTO.User;
using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO
{
    public class TypeConverter : ITypeConverter
    {
        public Models.BaseItem BaseItemDTOToBaseItem(NewBaseItemDTO baseItemDTO)
        {
            return new Models.BaseItem
            {
                Name = baseItemDTO.Name,
                Description = baseItemDTO.Description,
                IdCategory = baseItemDTO.IdCategory
            };
        }

        public BaseItemDTO BaseItemToBaseItemDTO(Models.BaseItem baseItem)
        {
            return new BaseItemDTO
            {
                IdBaseItem = baseItem.IdBaseItem,
                Name = baseItem.Name,
                Description = baseItem.Description,
                IdCategory = baseItem.IdCategory,
                CreatedDate = baseItem.CreatedDate,
                CategoryName = baseItem.IdCategoryNavigation.Name,
                Items = baseItem.Items.Select(item => ItemToItemDTO(item)).ToList()
            };
        }

        public InsertedBaseItemDTO? BaseItemToInsertedBaseItemDTO(Models.BaseItem baseItem)
        {
            if (baseItem == null) return null;
            return new InsertedBaseItemDTO
            {
                IdBaseItem = baseItem.IdBaseItem,
                Name = baseItem.Name,
                Description = baseItem.Description,
                IdCategory = baseItem.IdCategory,
                CreatedDate = baseItem.CreatedDate
            };
        }

        public ItemDTO ItemToItemDTO(Models.Item item)
        {
            return new ItemDTO
            {
                IdItem = item.IdItem,
                Size = item.Size,
                Color = item.Color,
                Stock = item.Stock,
                Price = item.Price,
                CreatedDate = item.CreatedDate,
                ItemImages = item.ItemImages.Select(itemImage => ItemImageToItemImageDTO(itemImage)).ToList(),
                BaseItem = BaseItemToInsertedBaseItemDTO(item.IdBaseItemNavigation)
            };
        }

        public ItemImageDTO ItemImageToItemImageDTO(Models.ItemImage itemImage)
        {
            return new ItemImageDTO
            {
                IdImage = itemImage.IdImage,
                IsMain = itemImage.IsMain,
                UrlImage = itemImage.UrlImage
            };
        }

        public CartLineDTO CartLineToCartLineDTO(Models.CartLine cartLine)
        {
            return new CartLineDTO
            {
                IdCart = cartLine.IdCart,
                IdItem = cartLine.IdItem,
                Quantity = cartLine.Quantity,
                IsPackageSent = cartLine.IsPackageSent,
                CreatedDate = cartLine.CreatedDate,
                Item = ItemToItemDTO(cartLine.IdItemNavigation)
            };
        }

        public Models.CartLine CartLineDTOToCartLine(NewCartLineDTO newCartLineDTO)
        {
            return new Models.CartLine
            {
                IdCart = newCartLineDTO.IdCart,
                IdItem = newCartLineDTO.IdItem,
                Quantity = newCartLineDTO.Quantity
            };
        }

        public CartDTO CartToCartDTO(Models.Cart cart)
        {
            return new CartDTO
            {
                IdCart = cart.IdCart,
                IdUser = cart.IdUser,
                IsBought = cart.IsBought,
                BoughtDate = cart.BoughtDate,
                CreatedDate = cart.CreatedDate,
                CartLines = cart.CartLines.Select(cartLine => CartLineToCartLineDTO(cartLine)).ToList()
            };
        }

        public Models.Category CategoryDTOToCategory(CategoryDTO categoryDTO)
        {
            return new Models.Category
            {
                Name = categoryDTO.Name,
                UrlImage = categoryDTO.UrlImage
            };
        }

        public CategoryDTO CategoryToCategoryDTO(Models.Category category)
        {
            return new CategoryDTO
            {
                IdCategory = category.IdCategory,
                Name = category.Name,
                UrlImage = category.UrlImage
            };
        }

        public Models.Category NewCategoryDTOToCategory(NewCategoryDTO newCategoryDTO)
        {
            return new Models.Category
            {
                Name = newCategoryDTO.Name,
                UrlImage = newCategoryDTO.UrlImage
            };
        }

        public Models.ItemImage ItemImageDTOToItemImage(NewItemImageDTO newItemImageDTO)
        {
            return new Models.ItemImage
            {
                IdItem = newItemImageDTO.IdItem,
                IsMain = newItemImageDTO.IsMain,
                UrlImage = newItemImageDTO.UrlImage
            };
        }

        public Models.Item ItemDTOToItem(NewItemDTO newItemDTO)
        {
            return new Models.Item
            {
                IdBaseItem = newItemDTO.IdBaseItem,
                Size = newItemDTO.Size,
                Color = newItemDTO.Color,
                Stock = newItemDTO.Stock,
                Price = newItemDTO.Price,
            };
        }

        public UserDTO UserToUserDTO(Models.User user)
        {
            return new UserDTO
            {
                IdUser = user.IdUser,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                CreatedDate = user.CreatedDate,
                Address = AddressToAddressDTO(user.Addresses.FirstOrDefault())
            };
        }

        public Models.User UserDTOToUser(NewUserDTO user)
        {
            return new Models.User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber
            };
        }

        public Models.Address NewAddressDTOToAddress(NewAddressDTO newAddressDTO)
        {
            return new Models.Address
            {
                StreetNumber = newAddressDTO.StreetNumber,
                AppartmentNumber = newAddressDTO.AppartmentNumber,
                StreetName = newAddressDTO.StreetName,
                City = newAddressDTO.City,
                State = newAddressDTO.State,
                ZipCode = newAddressDTO.ZipCode,
                Country = newAddressDTO.Country
            };
        }

        public AddressDTO AddressToAddressDTO(Models.Address address)
        {
            if (address == null) return null;
            return new AddressDTO
            {
                IdAddress = address.IdAddress,
                StreetNumber = address.StreetNumber,
                AppartmentNumber = address.AppartmentNumber,
                StreetName = address.StreetName,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country
            };
        }
    }
}
