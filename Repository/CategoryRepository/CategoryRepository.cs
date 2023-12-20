using marketplace_backend.DTO.Category;
using marketplace_backend.DTO.User;
using marketplace_backend.Models;
using marketplace_backend.Repositories;

namespace marketplace_backend.Repository.CategoryRepository
{
    public class CategoryRepository : BaseRepositorySQL<Category>, ICategoryRepository
    {
        public CategoryRepository(MarketplaceBackendDbContext context) : base(context) { }

        public async Task<Category?> UpdateOne(int idCategory, NewCategoryDTO newCategoryDTO)
        {
            Category? category = (from Category c in context.Set<Category>()
                          where c.IdCategory == idCategory
                          select c).FirstOrDefault();

            category.Name = newCategoryDTO.Name;
            category.UrlImage = newCategoryDTO.UrlImage;

            await SaveChangesAsync();

            return category;
        }
    }
}
