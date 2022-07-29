namespace MediaApp.Api.Registers.Builder;

public class MessageBusRegister : IWebApplicationBuilderRegister
{
    public void Register(WebApplicationBuilder builder)
    {
        // RegisterRabbitMQMessageBus(builder);

        RegisterKafkaMessageBus(builder);
    }

    private void RegisterRabbitMQMessageBus(WebApplicationBuilder builder)
    {
        var rabbitMQSettings = new RabbitMQSettings();
        builder.Configuration.Bind(nameof(RabbitMQSettings), rabbitMQSettings);

        builder.Services.AddSingleton<IMessageBusPublisher<MessageBusPostEntity>, RabbitMQMessageBusPublisher>(opt =>
        {
            return new RabbitMQMessageBusPublisher(rabbitMQSettings);
        });

        builder.Services.AddHostedService(opt =>
        {
            return new RabbitMQMessageBusConsumer(rabbitMQSettings, new EventProcessor());
        });
    }

    private void RegisterKafkaMessageBus(WebApplicationBuilder builder)
    {
        var kafkaSettings = new KafkaSettings();
        builder.Configuration.Bind(nameof(KafkaSettings), kafkaSettings);

        builder.Services.AddSingleton<IMessageBusPublisher<MessageBusPostEntity>, KafkaMessageBusPublisher>(opt =>
        {
            return new KafkaMessageBusPublisher(kafkaSettings);
        });

        builder.Services.AddHostedService(opt =>
        {
            return new KafkaMessageBusConsumer(kafkaSettings, new EventProcessor());
        });
    }
}