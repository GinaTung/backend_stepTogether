using Microsoft.AspNetCore.Mvc;
using stepTogether.Models;
using stepTogether.Utils;
using stepTogether.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace stepTogether.Controllers
{
    [ApiController]
    [Route("stepTogether/user/")]
    public class AuthController : ControllerBase
    {
        private readonly SupabaseService _supabase;
        private readonly JwtHelper _jwtHelper;

        public AuthController(SupabaseService supabase, JwtHelper jwtHelper)
        {
            _supabase = supabase;
            _jwtHelper = jwtHelper;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        [SwaggerOperation(Tags = new[] { "會員註冊、登入登出" })]
        public async Task<IActionResult> Signup([FromBody] SignupRequest req)
        {
            var existing = await _supabase.SupabaseClient
                .From<Member>()
                .Where(m => m.Email == req.Email)
                .Get();

            if (existing.Models.Any())
                return BadRequest(ApiResponse<string>.Fail("Email 已註冊"));

            var hash = PasswordHasher.HashPassword(req.Password);

            var newMember = new Member
            {
                Id = Guid.NewGuid(),
                Email = req.Email,
                PasswordHash = hash,
                Username = req.Username,
                FullName = req.FullName,
                Birthday = req.Birthday,
                Status = "正常",
                Role = "一般會員",
                ArticleCount = 0,
                CommentCount = 0,
                CreatedAt = DateTime.UtcNow
            };

            var insertResult = await _supabase.SupabaseClient
                .From<Member>()
                .Insert(newMember);

            return Ok(ApiResponse<string>.Ok(null, "註冊成功"));
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        [SwaggerOperation(Tags = new[] { "會員註冊、登入登出" })]
        [SwaggerResponse(200, "登入成功", typeof(ApiResponse<object>))]
        [SwaggerResponse(401, "帳號或密碼錯誤", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(401, typeof(UnauthorizedExample))]
        public async Task<IActionResult> Signin([FromBody] SigninRequest req)
        {
            var result = await _supabase.SupabaseClient
                .From<Member>()
                .Where(x => x.Email == req.Email)
                .Get();

            var user = result.Models.FirstOrDefault();

            if (user == null || !PasswordHasher.Verify(req.Password, user.PasswordHash))
                return Unauthorized(ApiResponse<string>.Fail("帳號或密碼錯誤"));

            var token = _jwtHelper.GenerateToken(user.Id.ToString(), user.Email, user.Role ?? "一般會員");

            return Ok(ApiResponse<object>.Ok(new { token }, "登入成功"));
        }

        [HttpPost("logout")]
        [SwaggerOperation(Tags = new[] { "會員註冊、登入登出" })]
        public IActionResult Logout()
        {
            return Ok(ApiResponse<string>.Ok(null, "成功登出"));
        }
    }
}
