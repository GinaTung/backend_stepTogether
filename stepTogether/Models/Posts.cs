using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Posts
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("title")]
    public string Title { get; set; }

    [Required]
    [Column("content")]
    public string Content { get; set; }

    [Column("createdat")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // 設定為當前時間

    [Column("updatedat")]
    public DateTime? UpdatedAt { get; set; }  // 更新時間在每次更新資料時設定

    [Required]
    [Column("author")]  // 映射小寫欄位名稱
    public string Author { get; set; }

    [Column("commentcount")]
    public int CommentCount { get; set; }
}
