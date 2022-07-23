namespace MediaApp.Application.ServiceResponses;

public abstract class BaseServiceResponse<T> : EntityWithErrors<string>
{
    public T? Payload { get; set; }
}
