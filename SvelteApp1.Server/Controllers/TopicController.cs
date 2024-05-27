using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SvelteApp1.Server.Data;
using SvelteApp1.Server.Models.Requests;
using SvelteApp1.Server.Models.Results;

namespace SvelteApp1.Server.Controllers
{
    [Route("topic")]
    public class TopicController : Controller
    {
        [HttpPost, Route("add"), Authorize(Roles = "admin")]
        public async Task<IActionResult> Add([FromBody] TopicRequest topic, [FromServices] ApplicationContext ctx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await ctx.Topics.AddAsync(new Topic() { Name = topic.Topic!});
            await ctx.SaveChangesAsync();
            return Ok();
        }
        [HttpGet, Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Get([FromServices] ApplicationContext ctx)
        {
            var topics = await ctx.Topics.ToListAsync();
            if (topics is null || !topics.Any())
            {
                return Json(new IsEmptyResult());
            }
            return Json(topics);
        }
        [HttpDelete, Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete([FromBody] DeleteTopicRequest topic, [FromServices] ApplicationContext ctx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var topicDel = await ctx.Topics.FindAsync(topic.TopicId);
            if (topicDel is null)
            {
                return StatusCode(500);
            }
            ctx.Topics.Remove(topicDel);
            await ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
