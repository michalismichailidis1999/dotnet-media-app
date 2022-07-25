namespace MediaApp.Api.Extensions;

public static class DatabaseExtensions
{
    public static void PrepareDatabase(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetService<DatabaseContext>();

            if (dbContext is not null)
            {
                Console.WriteLine("Applying migrations...");
                dbContext.Database.Migrate();
                Console.WriteLine("Migrations applied...");
            }
            else Console.WriteLine("ERROR: No DB Context found");
        }
    }
}