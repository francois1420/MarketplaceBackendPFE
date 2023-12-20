using marketplace_backend.DTO;
using marketplace_backend.DTO.Cart;
using marketplace_backend.DTO.CartLine;
using marketplace_backend.Models;
using marketplace_backend.Repository.CartLineRepository;
using marketplace_backend.Services.CartService;
using marketplace_backend.UnitsOfWork;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace marketplace_backend.Services.CartLineService
{
    public class CartLineService : ICartLineService
    {
        private ICartLineRepository _cartLineRepository;
        private ICartService _cartService;
        private ITypeConverter _typeConverter;

        public CartLineService(ITypeConverter typeConverter, ICartService cartService)
        {
            _cartLineRepository = new UnitOfWorkDB(new MarketplaceBackendDbContext()).CartLineRepository;
            _cartService = cartService;
            _typeConverter = typeConverter;
        }

        public async Task<CartLine?> GetOne(int idCart, int idItem)
        {
            IList<CartLine> cartLines = await _cartLineRepository.SearchForAsync(cl => cl.IdCart == idCart && cl.IdItem == idItem);
            return cartLines.FirstOrDefault();
        }

        public async Task<CartLine?> AddOne(int idUser, int idItem)
        {
            CartDTO? cartDTO = _cartService.GetActualCartByUserId(idUser);
            if (cartDTO == null) return null;
            int idCart = cartDTO.IdCart;

            CartLine cartLine = new CartLine { IdCart = idCart, IdItem = idItem };
            CartLine? insertedCartLine = await _cartLineRepository.InsertAsync(cartLine);

            if (insertedCartLine == null) return null;
            return insertedCartLine;
        }

        public async Task<CartLineDTO?> UpdateOne(int idUser, int idItem, int quantity)
        {
            if (quantity <= 0) return null;

            CartDTO? cartDTO = _cartService.GetActualCartByUserId(idUser);
            if (cartDTO == null) return null;

            CartLine? cartLine = await _cartLineRepository.UpdateOne(cartDTO.IdCart, idItem, quantity);
            if (cartLine == null) return null;

            CartLineDTO cartLineDTO = _typeConverter.CartLineToCartLineDTO(cartLine);
            return cartLineDTO;
        }

        public async Task<CartLineDTO?> UpdateOneItemSizeOrColor(int idUser, int idItem, string size, string color)
        {
            if (size == null || color == null) return null;

            CartDTO? cartDTO = _cartService.GetActualCartByUserId(idUser);
            if (cartDTO == null) return null;

            CartLine? cartLine = await _cartLineRepository.UpdateOneItemSizeOrColor(cartDTO.IdCart, idItem, size, color);
            if (cartLine == null) return null;

            CartLineDTO cartLineDTO = _typeConverter.CartLineToCartLineDTO(cartLine);
            return cartLineDTO;
        }


        public async Task<bool> DeleteOne(int idUser, int idItem)
        {
            CartDTO? cart = _cartService.GetActualCartByUserId(idUser);
            if (cart == null) return false;
            CartLine? cartLine = await _cartLineRepository.GetCartLine(cart.IdCart, idItem);
            if (cartLine == null) return false;
            await _cartLineRepository.DeleteAsync(cartLine);
            return true;
        }
    }
}
