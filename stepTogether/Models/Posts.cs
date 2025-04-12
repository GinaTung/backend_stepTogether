using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
[Table("posts")] // 👈 加上這行指定表名（小寫）
public class Posts
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
    [Column("author")]  // 映射小寫欄位名稱
    public string Author { get; set; }
    [Column("commentcount")]
    public int CommentCount { get; set; }
    [Column("createdat")]
    public DateTime CreatedAt { get; set; }
    [Column("updatedat")]
    public DateTime? UpdatedAt { get; set; }
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
    [Column("commentcount")]
    public int CommentCount { get; set; }
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
    [Column("commentcount")]
    public int CommentCount { get; set; }
    [Column("createdat")]
    public DateTime CreatedAt { get; set; }
    [Column("updatedat")]
    public DateTime? UpdatedAt { get; set; }
}
