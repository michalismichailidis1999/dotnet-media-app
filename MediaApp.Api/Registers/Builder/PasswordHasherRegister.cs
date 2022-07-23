namespace MediaApp.Api.Registers.Builder;

public class PasswordHasherRegister : IWebApplicationBuilderRegister
{
    public void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
    }
}
