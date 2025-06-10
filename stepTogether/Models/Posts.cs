using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static PostOutputDto;
[Table("posts")] 
public class Posts : BaseModel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("title")]
    public string Title { get; set; }

    [Required]
    [Column("content")]
    public string Content { get; set; }

    [Required]
    [Column("author")]
    public string Author { get; set; }

    [Required]
    [Column("category")]
    public string Category { get; set; }

    [Column("tags")]
    public List<string>? Tags { get; set; } = new() { "ALL" };


    [Column("commentcount")]
    public int CommentCount { get; set; }

    [Column("createdat")]
    public DateTime CreatedAt { get; set; }

    [Column("updatedat")]
    public DateTime? UpdatedAt { get; set; }
    [Required]
    [Column("status")]
    public string Status { get; set; }

    [Column("review_status")]
    [JsonPropertyName("review_status")]
    public string? ReviewStatus { get; set; }

    [Column("viewcount")]
    public int ViewCount { get; set; }

    [Column("likecount")]
    public int LikeCount { get; set; }

    //[Column("hidden_deleted_log", TypeName = "jsonb")]
    //public List<HiddenLog>? HiddenDeletedLog { get; set; } = new();

    [Column("image_url")]
    public List<string>? ImageUrl { get; set; }
}

public class PostInputDto
{
    [Required]
    [Column("title")]
    public string Title { get; set; }

    [Required]
    [Column("content")]
    public string Content { get; set; }

    [Required]
    [Column("author")]  // 映射小寫欄位名稱
    public string Author { get; set; }

    [Required]
    [Column("category")]
    public string Category { get; set; }
    [Required]
    [Column("status")]
    public string Status { get; set; }
}
public class PostOutputDto
{
    [Column("id")]
    public int Id { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("content")]
    public string Content { get; set; }
    [Column("author")]  // 映射小寫欄位名稱
    public string Author { get; set; }
    [Column("category")]
    public string Category { get; set; }
    [Column("tags")]
    public List<string>? Tags { get; set; } = new() { "ALL" };

    [Column("commentcount")]
    public int CommentCount { get; set; }
    [Column("createdat")]
    public DateTime CreatedAt { get; set; }
    [Column("updatedat")]
    public DateTime? UpdatedAt { get; set; }
    [Column("status")]
    public string Status { get; set; }

    [Column("review_status")]
    [JsonPropertyName("review_status")]
    public string ReviewStatus { get; set; }

    [Column("viewcount")]
    public int ViewCount { get; set; }

    [Column("likecount")]
    public int LikeCount { get; set; }

    //[Column("hidden_deleted_log", TypeName = "jsonb")]
    //public List<HiddenLog>? HiddenDeletedLog { get; set; } = new();


    [Column("image_url")]
    public List<string>? ImageUrl { get; set; }
}
public class HiddenLog
{
    public string Action { get; set; }  // 例如 "delete"
    public DateTime Timestamp { get; set; }
    public string User { get; set; }
}
