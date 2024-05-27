using Microsoft.AspNetCore.Mvc;
using SvelteApp1.Server.Models;
using HashLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SvelteApp1.Server.Data;
using SvelteApp1.Server.Models.Requests;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System;
using System.Runtime.InteropServices;
using SvelteApp1.Server.Models.Results;

namespace SvelteApp1.Server.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromServices] ApplicationContext ctx, [FromBody] LoginRequest login)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await ctx.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Name == login.Username && x.Password == HashStatic.GetHash256(login.Password!));
            if (user is null)
            {
                return BadRequest();
            }
            var claims = new List<Claim> { 
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Guid.ToString()), 
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            var isAdmin = new IsAdminResult();
            isAdmin.IsAdmin = user.Role.Name == "admin" ? true : false;
            return Json(isAdmin);
        }
        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromServices] ApplicationContext ctx, [FromBody] RegisterRequest register)
        {
            //var adminRole = new Role() { Name = "admin" };
            //var userRole = new Role() { Name = "user" };
            //var admin = new User() { Name = "admin", Guid = Guid.NewGuid(), Role = adminRole, Password = HashStatic.GetHash256("admin") };
            //ctx.Roles.AddRange([adminRole, userRole]);
            //ctx.Users.Add(admin);
            //var grade7 = new Grade() { GradeNumber = 7 };
            //var grade8 = new Grade() { GradeNumber = 8 };
            //var grade9 = new Grade() { GradeNumber = 9 };
            //var grade6 = new Grade() { GradeNumber = 10 };
            //var grade4 = new Grade() { GradeNumber = 11 };
            //ctx.Grades.AddRange([grade7, grade8, grade9, grade6, grade4]);
            //ctx.SaveChanges();
            //return Ok();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var role = await ctx.Roles.FirstOrDefaultAsync(x => x.Name == "user");
            if (role is null)
            {
                return StatusCode(500);
            }
            var user = new User()
            {
                Name = register.Username!,
                Guid = Guid.NewGuid(),
                Password = HashStatic.GetHash256(register.Password!),
                Role = role,
            };
            await ctx.Users.AddAsync(user);
            await ctx.SaveChangesAsync();
            return Ok();
        }
        [HttpGet, Route("logout"), Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Logout([FromServices] ApplicationContext ctx)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
