namespace MediaApp.Api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUser, User>();
        CreateMap<LoginUser, User>();
        CreateMap<User, UserResponse>();
        CreateMap<User, AuthUserResponse>();
    }
}