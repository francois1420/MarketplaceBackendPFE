using marketplace_backend.DTO.Cart;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace marketplace_backend.Services.CartService
{
    public interface ICartService
    {
        CartDTO? GetActualCartByUserId(int id);
        IList<CartDTO> GetHistoryCartsByUserId(int id);
        Task<CartDTO?> AddOne(int idUser);
        Task<bool> BuyCart(int idUser);
        string GetClientSecret(int idUser);
    }
}
