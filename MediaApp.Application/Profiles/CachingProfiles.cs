namespace MediaApp.Application.Profiles;

public class CachingProfiles : Profile
{
    public CachingProfiles()
    {
        CreateMap<User, UserCachingModel>();
        CreateMap<UserCachingModel, User>();
        CreateMap<Post, PostCachingModel>();
        CreateMap<PostCachingModel, Post>();
        CreateMap<PostInteraction, PostInteractionCachingModel>();
        CreateMap<PostInteractionCachingModel, PostInteraction>();
        CreateMap<Comment, CommentCachingModel>();
        CreateMap<CommentCachingModel, Comment>();
        CreateMap<CommentInteraction, CommentInteractionCachingModel>();
        CreateMap<CommentInteractionCachingModel, CommentInteraction>();
    }
}