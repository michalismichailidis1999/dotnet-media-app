namespace MediaApp.Api.Registers.Builder;

public class AutoMapperRegister : IWebApplicationBuilderRegister
{
    public void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Program), typeof(BaseService));
    }
}
