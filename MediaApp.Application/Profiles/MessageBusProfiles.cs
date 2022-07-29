namespace MediaApp.Application.Profiles;

public class MessageBusProfiles : Profile
{
    public MessageBusProfiles()
    {
        CreateMap<Post, MessageBusPostEntity>();
    }
}