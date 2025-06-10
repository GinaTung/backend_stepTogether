namespace stepTogether.Models
{
    public class CreateCommentDto
    {
        public int PostId { get; set; }
        public int PostAuthorId { get; set; }
        public string Content { get; set; }
        public int CommenterId { get; set; }
    }
}
