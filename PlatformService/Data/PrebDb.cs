using Microsoft.EntityFrameworkCore;
using PlatformService.Models;


namespace PlatformService.Data;

public static class PrebDb
{
    public static void PrebPopulation(IApplicationBuilder app, bool isProd)
    {
        using (var serviceScobe = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScobe.ServiceProvider.GetService<AppDbContext>(), isProd);
        }
    }

    private static void SeedData(AppDbContext appDbContext, bool isProd)
    {
        if (isProd)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                appDbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }

        }

        if (!appDbContext.Platforms.Any())
        {
            Console.WriteLine("--> Seeding data");

            appDbContext.Platforms.AddRange(
                new Platform() { Name = "Dot net", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "Kubernates", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
            );

            appDbContext.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> we have already data");
        }
    }
}