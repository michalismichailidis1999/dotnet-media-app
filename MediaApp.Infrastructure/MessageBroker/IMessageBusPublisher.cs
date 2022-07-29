namespace MediaApp.Infrastructure.MessageBroker;

public interface IMessageBusPublisher<T>
{
    void Publish(T message);
    void Dispose();
}