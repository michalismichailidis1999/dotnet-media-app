namespace MediaApp.Application.ServiceResponses;

public class UserServiceResponse : BaseServiceResponse<User>
{
    public string? Token { get; set; }
}
