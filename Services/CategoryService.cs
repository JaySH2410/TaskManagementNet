using TaskManagement.DTOs;
using TaskManagement.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        private CategoryDto ToDto(CategoryEntity entity)
        {
            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        private CategoryEntity FromCreateDto(CreateCategoryDto dto)
        {
            return new CategoryEntity
            {
                Name = dto.Name
            };
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all categories");
                var categories = await _categoryRepository.GetAllAsync();
                return categories.Select(ToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching categories");
                throw;
            }
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching category with Id = {Id}", id);
                var category = await _categoryRepository.GetByIdAsync(id);

                return category == null ? null : ToDto(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching category {Id}", id);
                throw;
            }
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            try
            {
                _logger.LogInformation("Creating new category [{Name}]", dto.Name);
                var entity = FromCreateDto(dto);
                var saved = await _categoryRepository.AddAsync(entity);

                _logger.LogInformation("Category created successfully with Id = {Id}", saved.Id);
                return ToDto(saved);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating category");
                throw;
            }
        }
    }
}
