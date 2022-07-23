namespace MediaApp.Infrastructure.Auth;

public interface IAuthenticationHandler
{
    string CreateAccessToken(User Payload);
}