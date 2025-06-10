using Microsoft.AspNetCore.Mvc;
using stepTogether.Data;
using stepTogether.Models;
using stepTogether.Utils;
using System;
using System.Threading.Tasks;

namespace stepTogether.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly SupabaseService _supabase;
        private readonly JwtHelper _jwtHelper;

        public CommentController(SupabaseService supabase, JwtHelper jwtHelper)
        {
            _supabase = supabase;
            _jwtHelper = jwtHelper;
        }

        [HttpPost]
        public async Task<ActionResult<CommentResponseDto>> CreateComment(CreateCommentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
            {
                return BadRequest("留言內容不得為空");
            }

            var comment = new Comment
            {
                PostId = dto.PostId,
                PostAuthorId = dto.PostAuthorId,
                Content = dto.Content,
                CommenterId = dto.CommenterId,
                CreatedAt = DateTime.UtcNow,
                LikeCount = 0,
                DislikeCount = 0,
                ReportCount = 0,
                Status = "normal"
            };

            try
            {
                var result = await _supabase.SupabaseClient
                    .From<Comment>()
                    .Insert(comment);

                // 從回傳結果取出 CommentId（Supabase 預設會返回插入資料）
                var insertedComment = result.Models.FirstOrDefault();
                if (insertedComment == null)
                    return StatusCode(500, "無法新增留言");

                var response = new CommentResponseDto
                {
                    CommentId = insertedComment.CommentId,
                    Message = "留言成功",
                    CreatedAt = insertedComment.CreatedAt
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"新增留言失敗：{ex.Message}");
            }
        }
    }
}
