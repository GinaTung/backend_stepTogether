using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stepTogether.Models
{
    [Table("members")]
    public class Member : BaseModel
    {
        [Column("id")]
        public Guid? Id { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("birthday")]
        public DateTime Birthday { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("status")]
        public string Status { get; set; } = "正常";

        [Column("role")]
        public string Role { get; set; } = "一般會員";

        [Column("article_count")]
        public int ArticleCount { get; set; } = 0;

        [Column("comment_count")]
        public int CommentCount { get; set; } = 0;
    }
}
