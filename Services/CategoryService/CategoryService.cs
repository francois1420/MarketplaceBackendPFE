using marketplace_backend.DTO;
using marketplace_backend.DTO.Category;
using marketplace_backend.Models;
using marketplace_backend.Repository.CategoryRepository;
using marketplace_backend.UnitsOfWork;

namespace marketplace_backend.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private ITypeConverter _typeConverter;

        public CategoryService(ITypeConverter typeConverter)
        {
            _categoryRepository = new UnitOfWorkDB(new MarketplaceBackendDbContext()).CategoryRepository;
            _typeConverter = typeConverter;
        }

        public async Task<IList<CategoryDTO>> GetAll()
        {
            return (await _categoryRepository.GetAllAsync()).Select(c => _typeConverter.CategoryToCategoryDTO(c)).ToList();
        }

        public async Task<CategoryDTO?> GetOne(int id)
        {
            Category? category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;
            return _typeConverter.CategoryToCategoryDTO(category);
        }

        public async Task<CategoryDTO?> AddOne(NewCategoryDTO newCategoryDTO)
        {
            Category category = _typeConverter.NewCategoryDTOToCategory(newCategoryDTO);
            Category? insertedCatgory = await _categoryRepository.InsertAsync(category);
            if (insertedCatgory == null) return null;
            return _typeConverter.CategoryToCategoryDTO(insertedCatgory);
        }

        public async Task<CategoryDTO?> UpdateOne(int id, NewCategoryDTO newCategoryDTO)
        {
            Category? updatedCategory = await _categoryRepository.UpdateOne(id, newCategoryDTO);
            if (updatedCategory == null) return null;
            CategoryDTO updatedCategoryDTO = _typeConverter.CategoryToCategoryDTO(updatedCategory);
            return updatedCategoryDTO;
        }

        public async void DeleteOne(int id)
        {
            await _categoryRepository.DeleteAsync(new Category() { IdCategory = id });
        }

        
    }
}
