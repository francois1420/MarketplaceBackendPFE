using marketplace_backend.DTO.Category;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.CategoryRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> UpdateOne(int idCategory, NewCategoryDTO newCategoryDTO);
    }
}
