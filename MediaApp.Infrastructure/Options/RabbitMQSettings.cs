namespace MediaApp.Infrastructure.Options;

public class RabbitMQSettings
{
    public string HostName { get; set; }
    public int Port { get; set; } = -1;
    public string Username { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
}