using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Interfaces;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(ApiResponse.Success(categories, "Categories retrieved successfully"));
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(ApiResponse.Success(category, "Category retrieved successfully"));
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var created = await _categoryService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetById),
                 new { id = created.Id },
                 ApiResponse.Success(created, "Category created successfully"));
        }
    }
}
