using RabbitMQ.Client.Events;

namespace MediaApp.Infrastructure.MessageBroker.RabbitMQ;

public class RabbitMQMessageBusConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IEventProcessor<MessageBusPostEntity> _eventProcessor;

    private const string _exchangeName = "media_app_exchange";
    private const string _routingKey = "";

    private string _queueName;

    public RabbitMQMessageBusConsumer(RabbitMQSettings settings, IEventProcessor<MessageBusPostEntity> eventProcessor)
    {
        _eventProcessor = eventProcessor;

        var factory = new ConnectionFactory
        {
            HostName = settings.HostName,
            Port = settings.Port,
            UserName = settings.Username,
            Password = settings.Password,
            VirtualHost = settings.VirtualHost
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: _routingKey);

            Console.WriteLine("{Consumer}: Connected to RabbitMQ...");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Couldn't connect to RabbitMQ: {e.Message}");
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (sender, args) =>
        {
            var body = args.Body;
            var messagePayload = 
                JsonSerializer.Deserialize<MessageBusPostEntity>(Encoding.UTF8.GetString(body.ToArray()));

            if (messagePayload is null) throw new Exception("Received event's message was null");

            _eventProcessor.Process(messagePayload);
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }

        base.Dispose();
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("RabbitMQ Consumer Connection Shutdown...");
    }
}