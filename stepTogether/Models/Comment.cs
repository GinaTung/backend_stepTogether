using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace stepTogether.Models
{
    [Table("comment")]
    public class Comment : BaseModel
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int PostAuthorId { get; set; }
        public string Content { get; set; }
        public int CommenterId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;
        public int ReportCount { get; set; } = 0;
        public string Status { get; set; } = "normal"; // normal / pending / hidden
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

}
