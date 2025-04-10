using Microsoft.AspNetCore.Mvc;
using stepTogether.Models;
using stepTogether.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace stepTogether.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public IActionResult Create(Posts post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAll), new { id = post.Id }, post);
        }
    }
}
