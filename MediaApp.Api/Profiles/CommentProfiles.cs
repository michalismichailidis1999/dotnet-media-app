namespace MediaApp.Api.Profiles;

public class CommentProfiles : Profile
{
    public CommentProfiles()
    {
        CreateMap<Comment, CommentResponse>();
        CreateMap<CommentInteraction, CommentInteractionResponse>();
    }
}