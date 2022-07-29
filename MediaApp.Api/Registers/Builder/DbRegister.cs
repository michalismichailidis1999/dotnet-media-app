using Media.DataAccess;

namespace MediaApp.Api.Registers.Builder;

public class DbRegister : IWebApplicationBuilderRegister
{
    public void Register(WebApplicationBuilder builder)
    {
        try
        {
            builder.Services.AddDbContext<DatabaseContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("SqlServer"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure
                        (
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        );
                    }
                )
            );
        }
        catch (Exception e)
        {
            Console.WriteLine($"Couldn't connect to SQL Server: {e.Message}");
        }
    }
}
