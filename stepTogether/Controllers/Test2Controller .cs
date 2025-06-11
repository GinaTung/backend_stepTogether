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
    [Route("api/[controller]")]
    public class Test2Controller : ControllerBase
    {
        private readonly SupabaseService _supabase;
        private readonly JwtHelper _jwtHelper;

        public Test2Controller(SupabaseService supabase, JwtHelper jwtHelper)
        {
            _supabase = supabase;
            _jwtHelper = jwtHelper;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile(string id)
        {
            var userId = id;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid token or missing user ID.");

            var result = await _supabase.SupabaseClient
                .From<Profile>()
                .Where(p => p.Id == userId)
                .Get();

            var profile = result.Models.FirstOrDefault();

            if (profile == null)
                return NotFound("Profile not found");

            return Ok(new
            {
                profile.Id,
                profile.Email,
                profile.Role
            });
        }


        [HttpPost("profile")]
        [Authorize]
        public async Task<IActionResult> CreateProfile([FromBody] CreateProfileDto input)
        {
            if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Role))
                return BadRequest("Email and Role are required.");

            var profile = new Profile
            {
                Email = input.Email,
                Role = input.Role
                // 不需要指定 Id，Supabase 會自動產生
            };

            var result = await _supabase.SupabaseClient
                .From<Profile>()
                .Insert(profile);

            return Ok(new
            {
                profile.Email,
                profile.Role
            });

        }


        [HttpPut("profile/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(string id, [FromBody] UpdateProfileDto updatedData)
        {
            if (string.IsNullOrEmpty(id) || updatedData == null)
                return BadRequest("Invalid data.");

            // 先取得原始資料
            var existingResult = await _supabase.SupabaseClient
                .From<Profile>()
                .Where(p => p.Id == id)
                .Get();

            var existingProfile = existingResult.Models.FirstOrDefault();
            if (existingProfile == null)
                return NotFound("Profile not found.");

            // 更新要修改的欄位
            existingProfile.Email = updatedData.Email;
            existingProfile.Role = updatedData.Role;

            var updateResult = await _supabase.SupabaseClient
                .From<Profile>()
                .Update(existingProfile);

            return Ok(new { message = "Profile updated", data = updateResult.Models.FirstOrDefault() });
        }
        [HttpDelete("profile/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProfile(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Missing profile ID.");

            var profile = new Profile { Id = id };

            var result = await _supabase.SupabaseClient
                .From<Profile>()
                .Delete(profile);

            return Ok(new { message = "Profile deleted", data = result.Models.FirstOrDefault() });
        }

    }

}
