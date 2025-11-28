using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTOs
{
    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters long")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MinLength(3, ErrorMessage = "Description must be at least 3 characters long")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("(Pending|InProgress|Done)",
             ErrorMessage = "Status must be either Pending, InProgress, or Done")]
        public string Status { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }
    }
}
