namespace MediaApp.Application.ServiceResponses;

public abstract class BaseContentServiceResponse<T, K> : BaseServiceResponse<T> where K : Interaction
{
    public List<K> Interactions { get; set; }
    public K Interaction { get; set; }
}