namespace MediaApp.Api.Profiles;

public class PostProfiles : Profile
{
    public PostProfiles()
    {
        CreateMap<Post, PostResponse>();
        CreateMap<PostInteraction, PostInteractionResponse>();
    }
}