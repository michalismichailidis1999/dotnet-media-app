namespace MediaApp.Api.Registers;

public interface IWebApplicationBuilderRegister : IRegister
{
    void Register(WebApplicationBuilder builder);
}
