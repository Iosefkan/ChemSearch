
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using SvelteApp1.Server.Data;

namespace SvelteApp1.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration;

            string connection = config.GetConnectionString("DefaultConnection")!;

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.LoginPath = "/auth/login";
                });
            builder.Services.AddAuthorization();

            builder.Services.AddResponseCompression(options => {
                options.EnableForHttps = true;
                options.Providers.Add(new GzipCompressionProvider(new GzipCompressionProviderOptions()));
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.EnsureMigrationOfContext<ApplicationContext>();

            app.UseHttpsRedirection();

            app.MapControllers();
            app.MapFallbackToFile("/index.html");
        
            app.Run();
        }
    }
}
