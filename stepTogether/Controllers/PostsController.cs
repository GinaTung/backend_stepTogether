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
    public class PostsController : ControllerBase
    {
        private readonly SupabaseService _supabase;
        private readonly JwtHelper _jwtHelper;

        public PostsController(SupabaseService supabase, JwtHelper jwtHelper)
        {
            _supabase = supabase;
            _jwtHelper = jwtHelper;
        }

        // GET: api/posts
        [HttpGet]
        [Authorize]
        [SwaggerOperation(Tags = new[] { "文章管理" })]  // 自訂分類名稱
        public async Task<IActionResult> GetAllPosts()
        {

            var result = await _supabase.SupabaseClient
                .From<Posts>()
                .Get();

            var posts = result.Models.FirstOrDefault();

            if (posts == null)
                return NotFound("Profile not found");

            return Ok(new
            {
                posts
            });
        }

    }
}
