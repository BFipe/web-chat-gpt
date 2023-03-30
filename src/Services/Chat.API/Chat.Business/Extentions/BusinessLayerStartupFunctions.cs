using Chat.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Extentions
{
    public static class BusinessLayerStartupFunctions
    {
        public static async Task MigrateDatabase(IServiceProvider service) 
        {
            using (var scope = service.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
                dbContext.Database.Migrate();
                await SeedDataAsync(dbContext);
            }
        }

        private static async Task SeedDataAsync(ChatDbContext dbContext)
        {
            if (!await dbContext.Roles.AnyAsync())
            {
                await dbContext.Roles.AddRangeAsync(new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            });

            await dbContext.SaveChangesAsync();
            }
        }
    }
}
