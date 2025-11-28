namespace TaskManagement.Models
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TaskEntity> Tasks { get; set; }
    }
}
