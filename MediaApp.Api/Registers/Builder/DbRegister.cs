using Media.DataAccess;

namespace MediaApp.Api.Registers.Builder;

public class DbRegister : IWebApplicationBuilderRegister
{
    public void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DatabaseContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
        );
    }
}
