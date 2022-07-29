namespace MediaApp.Infrastructure.MessageBroker.Kafka;

public class KafkaMessageBusPublisher : IMessageBusPublisher<MessageBusPostEntity>
{
    private readonly IProducer<Null, string> _producer;
    private readonly string _topic;

    private readonly ProducerConfig _config = new ProducerConfig
    {
        BootstrapServers = "localhost:9092"
    };

    public KafkaMessageBusPublisher(KafkaSettings kafkaSettings)
    {
        try
        {
            _config.BootstrapServers = kafkaSettings.BootstrapServers ?? _config.BootstrapServers;
            _producer = new ProducerBuilder<Null, string>(_config).Build();
            _topic = kafkaSettings.Topic;
        }
        catch (Exception e)
        {
            Console.WriteLine($"PUBLISHER: Couldn't connect to Kafka: {e.Message}");
        }
    }

    public async void Publish(MessageBusPostEntity post)
    {
        if(_producer is not null)
        {
            try
            {
                var messageValue = JsonSerializer.Serialize(post);
                await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = messageValue });
                Console.WriteLine($"Published message on topic:'{_topic}' with body:'{messageValue}'");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while publishing the message in kafka topic '{_topic}': {e.Message}");
            }
        }
        else
        {
            Console.WriteLine("Couldn't publish the message, channel is closed or there is no connection to RabbitMQ");
        }
    }

    public void Dispose()
    {
        Console.WriteLine("Disposed Kafka...");

        if (_producer is not null) _producer.Dispose();
    }
}