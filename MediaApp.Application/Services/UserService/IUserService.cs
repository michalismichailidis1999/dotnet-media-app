namespace MediaApp.Application.Services.UserService;

public interface IUserService
{
    Task<UserServiceResponse> Register(User user);

    Task<UserServiceResponse> Login(User user);

    Task<UserServiceResponse> UpdateUsername(Guid id, string username);

    Task<UserServiceResponse> UpdateEmail(Guid id, string email);

    Task<UserServiceResponse> UpdateFirstName(Guid id, string firstName);

    Task<UserServiceResponse> UpdateLastName(Guid id, string lastName);
    Task<UserServiceResponse> DeleteUser(Guid id);
}
