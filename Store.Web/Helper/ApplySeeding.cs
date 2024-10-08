﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository;

namespace Store.Web.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerfactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<StoreDbContext>();
                    var usermanger = services.GetRequiredService<UserManager<AppUser>>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerfactory);
                    await StoreIdentityContextSeed.SeedUserAsync(usermanger);
                }
                catch (Exception ex)
                {
                    var logger = loggerfactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
