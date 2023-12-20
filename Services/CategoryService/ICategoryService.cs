using marketplace_backend.DTO.Category;
using marketplace_backend.Models;

namespace marketplace_backend.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<IList<CategoryDTO>> GetAll();
        Task<CategoryDTO?> GetOne(int id);
        Task<CategoryDTO?> AddOne(NewCategoryDTO newCategoryDTO);
        Task<CategoryDTO?> UpdateOne(int id, NewCategoryDTO newCategoryDTO);
        void DeleteOne(int id);
    }
}
