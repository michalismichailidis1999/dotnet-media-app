namespace MediaApp.Api.Extensions;

public static class RegisterExtensions
{
    public static void Register(this WebApplicationBuilder builder, Type scanningType)
    {
        var registers = GetRegisters<IWebApplicationBuilderRegister>(scanningType);

        foreach (var register in registers)
        {
            register.Register(builder);
        }
    }

    public static void Register(this WebApplication app, Type scanningType)
    {
        var registers = GetRegisters<IWebApplicationRegister>(scanningType);

        foreach (var register in registers)
        {
            register.Register(app);
        }
    }

    private static IEnumerable<T> GetRegisters<T>(Type scanningType) where T :  IRegister
    {
        return scanningType.Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<T>();
    }
}
