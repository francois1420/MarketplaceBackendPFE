using marketplace_backend.DTO.CartLine;
using marketplace_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_backend.Services.CartLineService
{
    public interface ICartLineService
    {
        Task<CartLine?> AddOne(int idUser, int idItem);
        Task<CartLineDTO?> UpdateOne(int idUser, int idItem, int quantity);
        Task<bool> DeleteOne(int idUser, int idItem);
        Task<CartLine?> GetOne(int idCart, int idItem);
        Task<CartLineDTO?> UpdateOneItemSizeOrColor(int idUser, int idItem, string size, string color);
    }
}
