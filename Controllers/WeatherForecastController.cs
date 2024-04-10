using Microsoft.AspNetCore.Mvc;

namespace Rediscaching.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarksController : ControllerBase
    {
        private readonly IRedisCacheService _redisCacheService;

        public MarksController(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> SaveMarks(string userId, [FromBody] int marks)
        {
            if (marks < 0 || marks > 100)
            {
                return BadRequest("Marks should be between 0 and 100.");
            }

            var success = await _redisCacheService.SaveMarksAsync(userId, marks);
            if (success)
            {
                return Ok("Marks saved successfully.");
            }
            else
            {
                return StatusCode(500, "Failed to save marks.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetMarks(string userId)
        {
            var marks = await _redisCacheService.GetMarksAsync(userId);
            if (marks.HasValue)
            {
                return Ok($"Marks for user {userId}: {marks}");
            }
            else
            {
                return NotFound($"Marks for user {userId} not found.");
            }
        }
    }

}
