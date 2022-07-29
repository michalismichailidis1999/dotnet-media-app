namespace MediaApp.Infrastructure.MessageBroker.Kafka;

public class KafkaMessageBusConsumer : BackgroundService
{
    private readonly IConsumer<Null, string> _consumer;
    private readonly IEventProcessor<MessageBusPostEntity> _eventProcessor;

    private readonly ConsumerConfig _config = new ConsumerConfig
    {
        GroupId = "media-app-consumer-group",
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnableAutoCommit = false
    };

    public KafkaMessageBusConsumer(KafkaSettings kafkaSettings, IEventProcessor<MessageBusPostEntity> eventProcessor)
    {
        try
        {
            _config.BootstrapServers = kafkaSettings.BootstrapServers ?? _config.BootstrapServers;
            _consumer = new ConsumerBuilder<Null, string>(_config).Build();
            _consumer.Subscribe(kafkaSettings.Topic);
            _eventProcessor = eventProcessor;
        }
        catch (Exception e)
        {
            Console.WriteLine($"CONSUMER: Couldn't connect to Kafka: {e.Message}");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await Task.Yield();

            var i = 0;
            while(!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);

                if (result is not null && result.Message.Value is not null)
                {
                    var post = JsonSerializer.Deserialize<MessageBusPostEntity>(result.Message.Value);
                    _eventProcessor.Process(post!);
                }
                else
                {
                    Console.WriteLine("Received event's message was null");
                }

                if (i++ % 1000 == 0)
                {
                    _consumer.Commit();
                }
            }
        }
        catch (Exception e)
        {
            if(_consumer is not null)
            {
                _consumer.Close();
            }

            Console.WriteLine($"Error while consuming messages: {e.Message}");
        }
    }
}
