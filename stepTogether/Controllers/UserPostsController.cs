using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using stepTogether.Data;
using stepTogether.Utils;
using Supabase.Postgrest.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace stepTogether.Controllers
{
    [ApiController]
    [Route("stepTogether/user/[controller]")]
    public class UserPostsController : ControllerBase
    {
        private readonly SupabaseService _supabase;
        private readonly JwtHelper _jwtHelper;

        public UserPostsController(SupabaseService supabase, JwtHelper jwtHelper)
        {
            _supabase = supabase;
            _jwtHelper = jwtHelper;
        }

        // GET: api/userPosts
        [HttpGet]
        [Authorize]
        [SwaggerOperation(Tags = new[] { "使用者文章管理" })]
        public async Task<IActionResult> GetUserPosts()
        {
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userMail = _jwtHelper.GetMailFromToken(User);   // ✅ 從 token 中解析 user mail

            if (string.IsNullOrEmpty(userMail))
                return Unauthorized("Invalid token or missing user mail.");

            var result = await _supabase.SupabaseClient
                .From<Posts>()
                .Where(p => p.UserMail == userMail)
                .Get();

            var posts = result.Models;

            if (posts == null || !posts.Any())
                return NotFound("No posts found for this user.");

            return Ok(new
            {
                posts
            });
        }


        // POST: api/UserPosts
        [HttpPost]
        [Authorize]
        [SwaggerOperation(Tags = new[] { "使用者文章管理" })]
        public async Task<IActionResult> CreateUserPosts([FromBody] PostInputDto dto)
        {
            var userMail = _jwtHelper.GetMailFromToken(User); // 取得使用者信箱
            if (string.IsNullOrEmpty(userMail))
                return Unauthorized("Invalid token or missing user mail.");

            var post = new Posts
            {
                Title = dto.Title,
                Content = dto.Content,
                Author = dto.Author,
                CreatedAt = DateTime.UtcNow,
                Category = dto.Category,
                Status = dto.Status,
                UserMail = userMail, // ⭐ 加入 usermail
                Tags = dto.Tags,
                ImageUrl = dto.ImageUrl,
                ReviewStatus = dto.ReviewStatus,
            };

            var result = await _supabase.SupabaseClient
                .From<Posts>()
                .Insert(post); // ❗別忘了插入資料

            // 回傳結果
            var output = new PostOutputDto
            {
                Title = post.Title,
                Content = post.Content,
                Author = post.Author,
                CommentCount = post.CommentCount,
                CreatedAt = post.CreatedAt,
                Category = post.Category,
                UpdatedAt = post.UpdatedAt,
                Tags = dto.Tags,
                Status = post.Status,
                ReviewStatus = post.ReviewStatus,
                ImageUrl = post.ImageUrl
            };

            return Ok(output);
        }


        // PUT: api/UserPosts
        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(Tags = new[] { "使用者文章管理" })]
        public async Task<IActionResult> UpdateUserPosts(int id, [FromBody] PostUpdateDto dto)
        {
            var userMail = _jwtHelper.GetMailFromToken(User);
            if (string.IsNullOrEmpty(userMail))
                return Unauthorized("Invalid token or missing user mail.");

            var existingResult = await _supabase.SupabaseClient
                .From<Posts>()
                .Where(p => p.Id == id && p.UserMail == userMail)
                .Get();

            var post = existingResult.Models.FirstOrDefault();
            if (post == null)
                return NotFound("Post not found.");

            // 有值就更新
            if (!string.IsNullOrEmpty(dto.Title)) post.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.Content)) post.Content = dto.Content;
            if (!string.IsNullOrEmpty(dto.Author)) post.Author = dto.Author;
            if (!string.IsNullOrEmpty(dto.Category)) post.Category = dto.Category;
            if (!string.IsNullOrEmpty(dto.Status)) post.Status = dto.Status;
            if (!string.IsNullOrEmpty(dto.ImageUrl)) post.ImageUrl = new List<string> { dto.ImageUrl };

            // ✅ 只有有傳入 tags 才更新
            if (dto.Tags != null)
            {
                post.Tags = dto.Tags.Distinct().ToList();
            }

            post.UpdatedAt = DateTime.UtcNow;

            await _supabase.SupabaseClient
                .From<Posts>()
                .Update(post);

            return Ok(post);
        }



    }
}
