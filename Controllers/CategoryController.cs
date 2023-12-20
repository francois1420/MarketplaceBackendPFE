using Microsoft.AspNetCore.Mvc;
using marketplace_backend.Services.CategoryService;
using marketplace_backend.DTO.Category;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace marketplace_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]"), Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IList<CategoryDTO>> GetCategories()
        {
            return await _categoryService.GetAll();
        }

        [HttpGet("{id_category}"), AllowAnonymous]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id_category)
        {
            CategoryDTO? cat = await _categoryService.GetOne(id_category);
            if (cat == null) return NotFound();
            return Ok(cat);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] NewCategoryDTO newCategoryDTO)
        {
            if (!AuthAdmin()) return Unauthorized();
            CategoryDTO? insertedCategory = await _categoryService.AddOne(newCategoryDTO);
            if (insertedCategory == null) return Conflict();
            return Created("", insertedCategory);
        }

        [HttpPut("{id_category}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id_category, [FromBody] NewCategoryDTO newCategoryDTO)
        {
            if (!AuthAdmin()) return Unauthorized();
            CategoryDTO? updatedCategoryDTO = await _categoryService.UpdateOne(id_category, newCategoryDTO);
            if (updatedCategoryDTO != null) return Ok(updatedCategoryDTO);
            return NotFound();
        }

        [HttpDelete("{id_category}")]
        public IActionResult DeleteCategory([FromRoute] int id_category)
        {
            if (!AuthAdmin()) return Unauthorized();
            _categoryService.DeleteOne(id_category);
            return Ok();
        }

        private bool AuthAdmin()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string userRole = claimsIdentity.FindFirst("Role").Value;
            if (userRole != "admin")
            {
                return false;
            }
            return true;
        }
    }
}
