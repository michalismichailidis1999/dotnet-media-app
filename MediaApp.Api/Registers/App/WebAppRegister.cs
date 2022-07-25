namespace MediaApp.Api.Registers.App;

public class WebAppRegister : IWebApplicationRegister
{
    public void Register(WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.PrepareDatabase();
    }
}
