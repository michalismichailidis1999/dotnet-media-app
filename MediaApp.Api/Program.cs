var builder = WebApplication.CreateBuilder(args);
{
    builder.Register(typeof(Program));
}

var app = builder.Build();
{
    app.Register(typeof(Program));
}

app.Run();
