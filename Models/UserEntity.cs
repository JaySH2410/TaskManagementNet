namespace TaskManagement.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
