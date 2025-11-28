using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTOs
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [MinLength(3, ErrorMessage = "Category name must be at least 3 characters long")]
        public string Name { get; set; }
    }
}
