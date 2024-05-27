﻿using Microsoft.EntityFrameworkCore;

namespace SvelteApp1.Server
{
    public static class EnsureMigration
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using (var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<T>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
