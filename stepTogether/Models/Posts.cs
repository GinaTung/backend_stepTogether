using Newtonsoft.Json;
using Supabase.Postgrest;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // for JsonPropertyName in Output DTO
using Supabase.Postgrest.Attributes;

using static PostOutputDto;

[Table("posts")]
public class Posts : BaseModel
{
    [PrimaryKey("id", false)]
    [JsonProperty("id")]
    public int Id { get; set; }

    [Required]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Required]
    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("author")]
    public string Author { get; set; }

    [Required]
    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("tags")]
    public List<string>? Tags { get; set; }


    [JsonProperty("commentcount")]
    public int CommentCount { get; set; }

    [JsonProperty("createdat")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedat")]
    public DateTime? UpdatedAt { get; set; }

    [Required]
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("review_status")]
    public string? ReviewStatus { get; set; }

    [JsonProperty("viewcount")]
    public int ViewCount { get; set; }

    [JsonProperty("likecount")]
    public int LikeCount { get; set; }

    //[JsonProperty("hidden_deleted_log")]
    //public List<HiddenLog>? HiddenDeletedLog { get; set; } = new();

    [JsonProperty("image_url")]
    public List<string>? ImageUrl { get; set; }

    [JsonProperty("usermail")]
    [Column("usermail")]
    public string? UserMail { get; set; } = string.Empty;
}
public class PostInputDto
{
    [Required]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Required]
    [JsonProperty("content")]
    public string Content { get; set; }

    [Required]
    [JsonProperty("author")]
    public string Author { get; set; }

    [Required]
    [JsonProperty("category")]
    public string Category { get; set; }

    [Required]
    [JsonProperty("status")]
    public string Status { get; set; }
    [JsonProperty("tags")]
    public List<string>? Tags { get; set; }

    [JsonProperty("image_url")]
    public List<string>? ImageUrl { get; set; }

    [JsonProperty("review_status")]
    public string? ReviewStatus { get; set; }
}

public class PostUpdateDto
{
    [JsonProperty("title")]
    public string? Title { get; set; }
    [JsonProperty("content")]
    public string? Content { get; set; }
    [JsonProperty("tags")]
    public List<string>? Tags { get; set; }
    public string? Author { get; set; }
    [JsonProperty("category")]
    public string? Category { get; set; }
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("image_url")]
    public List<string>? ImageUrl { get; set; }
    [JsonProperty("review_status")]
    public string? ReviewStatus { get; set; }
}
public class PostOutputDto
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("author")]
    public string Author { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("tags")]
    public List<string>? Tags { get; set; }


    [JsonProperty("commentcount")]
    public int CommentCount { get; set; }

    [JsonProperty("createdat")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedat")]
    public DateTime? UpdatedAt { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonPropertyName("review_status")] // 使用 System.Text.Json 命名
    public string ReviewStatus { get; set; }

    [JsonProperty("viewcount")]
    public int ViewCount { get; set; }

    [JsonProperty("likecount")]
    public int LikeCount { get; set; }

    //[JsonProperty("hidden_deleted_log")]
    //public List<HiddenLog>? HiddenDeletedLog { get; set; } = new();

    [JsonProperty("image_url")]
    public List<string>? ImageUrl { get; set; }
}
public class HiddenLog
{
    public string Action { get; set; }
    public DateTime Timestamp { get; set; }
    public string User { get; set; }
}
