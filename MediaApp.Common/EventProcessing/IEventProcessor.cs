namespace MediaApp.Common.EventProcessing;

public interface IEventProcessor<T>
{
    void Process(T received);
}