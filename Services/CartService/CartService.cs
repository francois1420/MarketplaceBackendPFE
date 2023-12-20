using marketplace_backend.DTO;
using marketplace_backend.DTO.Cart;
using marketplace_backend.Models;
using marketplace_backend.Repository.CartRepository;
using marketplace_backend.UnitsOfWork;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace marketplace_backend.Services.CartService
{
    public class CartService : ICartService
    {
        private ICartRepository _cartRepository;
        private ITypeConverter _typeConverter;

        public CartService(ITypeConverter typeConverter)
        {
            _cartRepository = new UnitOfWorkDB(new MarketplaceBackendDbContext()).CartRepository;
            _typeConverter = typeConverter;
        }

        public async Task<CartDTO?> AddOne(int idUser)
        {
            Cart? cart = await _cartRepository.InsertAsync(new Cart { IdUser = idUser });
            if (cart == null) return null;
            return _typeConverter.CartToCartDTO(cart);
        }

        public CartDTO? GetActualCartByUserId(int idUser)
        {
            Cart? cart = _cartRepository.GetActualCartByUserId(idUser);
            if (cart == null) return null;
            return _typeConverter.CartToCartDTO(cart);
        }

        public IList<CartDTO> GetHistoryCartsByUserId(int idUser)
        {
            IList<Cart> listCarts = _cartRepository.GetHistoryCartsByUserId(idUser);
            return listCarts.Select(c => _typeConverter.CartToCartDTO(c)).ToList();
        }

        public async Task<bool> BuyCart(int idUser)
        {
            // Get cart with user id
            Cart? cart = _cartRepository.GetActualCartByUserId(idUser);
            if (cart == null || cart.CartLines.Count() == 0) return false;
            // Change cart isBought status to true and boughtDate to now + change stock
            Cart? updatedCart = await _cartRepository.BuyCart(cart);
            if (updatedCart == null) return false;
            // Add new cart to user
            CartDTO? insertedCart = await AddOne(idUser);
            if (insertedCart == null) return false;
            return true;
        }

        public string GetClientSecret(int idUser)
        {
            Cart? cart = _cartRepository.GetActualCartByUserId(idUser);
            if (cart == null || cart.CartLines.Count() == 0) return null;
            PaymentIntent paymentIntent = CreatePaymentIntent(cart);
            return paymentIntent.ClientSecret;
        }

        private PaymentIntent CreatePaymentIntent(Cart cart)
        {
            long totalAmount = 0;
                
            foreach (CartLine c in cart.CartLines)
            {
                totalAmount += Convert.ToInt64(c.IdItemNavigation.Price) * c.Quantity;
            }

            var options = new PaymentIntentCreateOptions
            {
                Amount = totalAmount*100,
                Currency = "eur",
                PaymentMethodTypes = new List<string> { "card" },
            };

            var service = new PaymentIntentService();

            return service.Create(options);
        }
    }
}
