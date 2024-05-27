using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SvelteApp1.Server.Data;
using SvelteApp1.Server.Models.Requests;
using Microsoft.EntityFrameworkCore;
using SvelteApp1.Server.Models.Results;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace SvelteApp1.Server.Controllers
{
    [Route("videos")]
    public class VideoController : Controller
    {
        [HttpGet, Authorize(Roles = "user, admin")]
        public async Task<IActionResult> GetVideos([FromQuery] FilterRequest filter, [FromServices] ApplicationContext ctx)
        {
            bool noFilterGrade = filter.GradeId is null || !filter.GradeId.Any();
            bool noFilterTopic = filter.Topicid is null || !filter.Topicid.Any();
            if (string.IsNullOrEmpty(filter.Name))
            {
                filter.Name = "";
            }
            VideoResult[] videos = Array.Empty<VideoResult>();
            if (noFilterGrade && noFilterTopic)
            {
                videos = await ctx.Videos.Include(x => x.Grade).Include(x => x.Topic).Where(x => x.Title.ToUpper().Contains(filter.Name.ToUpper())).Select(x => new VideoResult()
                {
                    Id = x.Id,
                    Link = x.Link,
                    Title = x.Title,
                    Grade = x.Grade.GradeNumber,
                    Topic = x.Topic.Name,
                }).ToArrayAsync();
            }
            if (noFilterGrade && !noFilterTopic)
            {
                videos = await ctx.Videos.Include(x => x.Grade).Include(x => x.Topic).Where(x => filter.Topicid!.Contains(x.TopicId) && x.Title.ToUpper().Contains(filter.Name.ToUpper())).Select(x => new VideoResult()
                {
                    Id = x.Id,
                    Link = x.Link,
                    Title = x.Title,
                    Grade = x.Grade.GradeNumber,
                    Topic = x.Topic.Name,
                }).ToArrayAsync();
            }
            if (!noFilterGrade && noFilterTopic)
            {
                videos = await ctx.Videos.Include(x => x.Grade).Include(x => x.Topic).Where(x => filter.GradeId!.Contains(x.GradeId) && x.Title.ToUpper().Contains(filter.Name.ToUpper())).Select(x => new VideoResult()
                {
                    Id = x.Id,
                    Link = x.Link,
                    Title = x.Title,
                    Grade = x.Grade.GradeNumber,
                    Topic = x.Topic.Name,
                }).ToArrayAsync();
            }
            if (!noFilterGrade && !noFilterTopic)
            {
                videos = await ctx.Videos.Include(x => x.Grade).Include(x => x.Topic).Where(x => filter.GradeId!.Contains(x.GradeId) && filter.Topicid!.Contains(x.TopicId) && x.Title.ToUpper().Contains(filter.Name.ToUpper())).Select(x => new VideoResult()
                {
                    Id = x.Id,
                    Link = x.Link,
                    Title = x.Title,
                    Grade = x.Grade.GradeNumber,
                    Topic = x.Topic.Name,
                }).ToArrayAsync();
            }
            if (videos is null || !videos.Any())
            {
                return Json(new IsEmptyResult());
            }
            return Json(videos);
        }
        [HttpGet, Route("favourite"), Authorize(Roles = "user, admin")]
        public async Task<IActionResult> GetFavourite([FromQuery] FilterRequest filter, [FromServices] ApplicationContext ctx)
        {
            var guid = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            bool succ = Guid.TryParse(guid, out var parsedGuid);
            if (guid is null || succ == false)
            {
                return StatusCode(500);
            }
            var user = await ctx.Users.FindAsync(parsedGuid);
            if (user is null)
            {
                return StatusCode(500);
            }
            bool noFilterGrade = filter.GradeId is null || !filter.GradeId.Any();
            bool noFilterTopic = filter.Topicid is null || !filter.Topicid.Any();
            if (string.IsNullOrEmpty(filter.Name))
            {
                filter.Name = "";
            }
            VideoResult[] videos = Array.Empty<VideoResult>();
            if (noFilterGrade && noFilterTopic)
            {
                videos = await ctx.Favourites.Include(x => x.Video).Include(x => x.Video.Grade).Include(x => x.Video.Topic).Where(x => x.UserGuid == user.Guid && x.Video.Title.ToUpper().Contains(filter.Name.ToUpper())).Select(x => new VideoResult()
                {
                    Id = x.Video.Id,
                    Link = x.Video.Link,
                    Title = x.Video.Title,
                    Grade = x.Video.Grade.GradeNumber,
                    Topic = x.Video.Topic.Name,
                }).ToArrayAsync();
            }
            if (noFilterGrade && !noFilterTopic)
            {
                videos = await ctx.Favourites.Include(x => x.Video).Include(x => x.Video.Grade).Include(x => x.Video.Topic).Where(x => x.UserGuid == user.Guid && filter.Topicid!.Contains(x.Video.TopicId) && x.Video.Title.ToUpper().Contains(filter.Name.ToUpper())).Select(x => new VideoResult()
                {
                    Id = x.Video.Id,
                    Link = x.Video.Link,
                    Title = x.Video.Title,
                    Grade = x.Video.Grade.GradeNumber,
                    Topic = x.Video.Topic.Name,
                }).ToArrayAsync();
            }
            if (!noFilterGrade && noFilterTopic)
            {
                videos = await ctx.Favourites.Include(x => x.Video).Include(x => x.Video.Grade).Include(x => x.Video.Topic).Where(x => x.UserGuid == user.Guid && filter.GradeId!.Contains(x.Video.GradeId) && x.Video.Title.ToUpper().Contains(filter.Name.ToUpper())).Select(x => new VideoResult()
                {
                    Id = x.Video.Id,
                    Link = x.Video.Link,
                    Title = x.Video.Title,
                    Grade = x.Video.Grade.GradeNumber,
                    Topic = x.Video.Topic.Name,
                }).ToArrayAsync();
            }
            if (!noFilterGrade && !noFilterTopic)
            {
                videos = await ctx.Favourites.Include(x => x.Video).Include(x => x.Video.Grade).Include(x => x.Video.Topic).Where(x => x.UserGuid == user.Guid && filter.Topicid!.Contains(x.Video.TopicId) && filter.GradeId!.Contains(x.Video.GradeId) && x.Video.Title.ToUpper().Contains(filter.Name.ToUpper())).Select(x => new VideoResult()
                {
                    Id = x.Video.Id,
                    Link = x.Video.Link,
                    Title = x.Video.Title,
                    Grade = x.Video.Grade.GradeNumber,
                    Topic = x.Video.Topic.Name,
                }).ToArrayAsync();
            }
            if (videos is null || !videos.Any())
            {
                return Json(new IsEmptyResult());
            }
            return Json(videos);
        }
        [HttpPost, Route("to_favourite"), Authorize(Roles = "user, admin")]
        public async Task<IActionResult> ToFavourite([FromBody] FavouriteRequest favourite, [FromServices] ApplicationContext ctx)
        {
            var guid = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            bool succ = Guid.TryParse(guid, out var parsedGuid);
            if (guid is null || succ == false)
            {
                return StatusCode(500);
            }
            var user = await ctx.Users.FindAsync(parsedGuid);
            if (user is null)
            {
                return StatusCode(500);
            }
            var video = await ctx.Videos.FindAsync(favourite.Id);
            if (video is null)
            {
                return StatusCode(500);
            }
            var fav = await ctx.Favourites.FirstOrDefaultAsync(x => x.UserGuid == user.Guid && x.VideoId == video.Id);
            if (fav is not null)
            {
                ctx.Favourites.Remove(fav);
                await ctx.SaveChangesAsync();
                return Json(new IsDeleted() { Deleted = true });
            }
            await ctx.Favourites.AddAsync(new Favourite()
            {
                User = user,
                UserGuid = user.Guid,
                Video = video,
                VideoId = video.Id,
            });
            await ctx.SaveChangesAsync();
            return Json(new IsDeleted());
        }
        [HttpPost, Route("add"), Authorize(Roles = "admin")]
        public async Task<IActionResult> Add([FromBody] VideoRequest video, [FromServices] ApplicationContext ctx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await ctx.Videos.AddAsync(new Video()
            {
                Link = video.Link!,
                Title = video.Title!,
                GradeId = video.GradeId,
                TopicId = video.TopicId,
            });
            await ctx.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete, Route("delete"), Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete([FromBody] DeleteVideoRequest video, [FromServices] ApplicationContext ctx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var delVideo = await ctx.Videos.FindAsync(video.Id);
            if (delVideo is null)
            {
                return StatusCode(500);
            }
            ctx.Videos.Remove(delVideo);
            await ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
