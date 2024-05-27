using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SvelteApp1.Server.Data;
using SvelteApp1.Server.Models.Results;

namespace SvelteApp1.Server.Controllers
{
    [Route("grade")]
    public class GradeController : Controller
    {
        [HttpGet, Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Get([FromServices] ApplicationContext ctx)
        {
            var grades = await ctx.Grades.ToListAsync();
            if (grades is null || !grades.Any())
            {
                return Json(new IsEmptyResult());
            }
            return Json(grades);
        }
    }
}
