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

    }

}
