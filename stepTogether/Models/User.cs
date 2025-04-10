using System.ComponentModel.DataAnnotations.Schema;

namespace stepTogether.Models
{
    public class User
    {
        [Column("id")]  // 映射到資料庫中的 id 欄位
        public int Id { get; set; }
        [Column("username")]
        public string Username { get; set; } = string.Empty;
        [Column("email")]
        public string? Email { get; set; }
        [Column("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
