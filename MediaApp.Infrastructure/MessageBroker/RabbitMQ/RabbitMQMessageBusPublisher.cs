namespace MediaApp.Infrastructure.MessageBroker.RabbitMQ;

public class RabbitMQMessageBusPublisher : IMessageBusPublisher<MessageBusPostEntity>
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    private const string _exchangeName = "media_app_exchange";
    private const string _routingKey = "";

    public RabbitMQMessageBusPublisher(RabbitMQSettings settings)
    {
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
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            Console.WriteLine("{Publisher}: Connected to RabbitMQ...");
        }
        catch (Exception e)
        {
            Console.WriteLine($"PUBLISHER: Couldn't connect to RabbitMQ: {e.Message}");
        }
    }

    public void Publish(MessageBusPostEntity post)
    {
        if (_connection is not null && _connection.IsOpen)
        {
            try
            {
                var message = JsonSerializer.Serialize(post);

                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(
                    exchange: _exchangeName,
                    routingKey: _routingKey,
                    basicProperties: null,
                    body: body
                );

                Console.WriteLine(
                    $"Published Message on exchange:'{_exchangeName}', routingKey:'{_routingKey}' and body:'{message}'"
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"Couldn't publishe the message on exchange='{_exchangeName}' with routingKey='{_routingKey}': {e.Message}"
                );
            }
        }
        else
        {
            Console.WriteLine("Couldn't publish the message, channel is closed or there is no connection to RabbitMQ");
        }
    }

    public void Dispose()
    {
        Console.WriteLine("RabbitMQ Disposed...");

        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("RabbitMQ Publisher Connection Shutdown...");
    }
}