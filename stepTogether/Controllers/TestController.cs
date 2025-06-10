using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stepTogether.Data;
using stepTogether.Models;
using System;
using System.Threading.Tasks;

namespace stepTogether.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly SupabaseService _supabase;

        public TestController(SupabaseService supabase)
        {
            _supabase = supabase;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTest([FromBody] Test test)
        {
            test.Id = Guid.NewGuid(); // 產生新的 GUID

            try
            {
                // 直接用 SupabaseClient 插入資料
                var response = await _supabase.SupabaseClient
                    .From<Test>()
                    .Insert(test);

                return Ok(response.Models); // 回傳新增後的資料
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTest(Guid id)
        {
            try
            {
                var response = await _supabase.SupabaseClient
                    .From<Test>()
                    .Where(t => t.Id == id)
                    .Get();

                var test = response.Models.Count > 0 ? response.Models[0] : null;

                if (test == null)
                    return NotFound();

                return Ok(test);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
