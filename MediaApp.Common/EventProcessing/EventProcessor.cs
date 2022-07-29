namespace MediaApp.Common.EventProcessing;

public class EventProcessor : IEventProcessor<MessageBusPostEntity>
{
    public EventProcessor()
    {
    }

    public void Process(MessageBusPostEntity post)
    {
        Console.WriteLine($"Received new created post with id={post.Id} and text='{post.Text}'");
    }
}
