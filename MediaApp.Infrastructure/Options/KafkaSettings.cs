namespace MediaApp.Infrastructure.Options;

public class KafkaSettings
{
    public string Topic { get; set; }
    public string BootstrapServers { get; set; }
}