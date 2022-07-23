namespace MediaApp.Api.Registers.Builder;

public class ControllersRegister : IWebApplicationBuilderRegister
{
    public void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(config =>
        {
            config.Filters.Add(typeof(ExceptionHandler));
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                return new ValidationFailedResult(context.ModelState);
            };
        });

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        builder.Services.AddEndpointsApiExplorer();
    }
}
