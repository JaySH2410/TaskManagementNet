namespace TaskManagement.Models
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }

        public CategoryEntity Category { get; set; }
        //public int UserId { get; set; }          
        //public UserEntity User { get; set; }     
    }
}
