namespace MediaApp.Api.Registers.Builder;

public class CachingRegister : IWebApplicationBuilderRegister
{
    public void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IConnectionMultiplexer>(
            opt => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"))
        );

        builder.Services.AddScoped<ICachingDB, RedisCachingDB>();
    }
}