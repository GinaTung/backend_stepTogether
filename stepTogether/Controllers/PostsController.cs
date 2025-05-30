using Microsoft.AspNetCore.Mvc;
using stepTogether.Models;
using stepTogether.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace stepTogether.Controllers
{
    [Authorize]
    [ApiController]
    [Route("stepTogether/user/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly StepTogetherDbContext _context;

        public PostsController(StepTogetherDbContext context)
        {
            _context = context;
        }

        // GET: api/posts
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "會員文章管理" })]  // 自訂分類名稱
        public IActionResult GetAll()
        {
            var posts = _context.Posts.ToList();
            return Ok(posts);
        }

        // POST: api/posts
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "會員文章管理" })]  // 自訂分類名稱
        public async Task<IActionResult> CreatePost([FromBody] PostInputDto dto)
        {
            var post = new Posts
            {
                Title = dto.Title,
                Content = dto.Content,
                Author = dto.Author,
                CreatedAt = DateTime.UtcNow,
                Category = dto.Category,
                Status = dto.Status,
                //HiddenDeletedLog = dto.HiddenDeletedLog ?? new()
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            // 轉換成輸出 DTO 回傳
            var output = new PostOutputDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Author = post.Author,
                CommentCount = post.CommentCount,
                CreatedAt = post.CreatedAt,
                Category=post.Category,
                UpdatedAt= post.UpdatedAt,
                Status = post.Status,
                ReviewStatus = post.ReviewStatus,
                ImageUrl = post.ImageUrl
            };

            return Ok(output);
        }

    }
}
