using Microsoft.AspNetCore.Mvc;
using stepTogether.Models;
using stepTogether.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace stepTogether.Controllers
{
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
        public IActionResult GetAll()
        {
            var posts = _context.Posts.ToList();
            return Ok(posts);
        }

        // POST: api/posts
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostInputDto dto)
        {
            var post = new Posts
            {
                Title = dto.Title,
                Content = dto.Content,
                Author = dto.Author,
                CommentCount = dto.CommentCount,
                CreatedAt = DateTime.UtcNow
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
                CreatedAt = post.CreatedAt
            };

            return Ok(output);
        }

    }
}
