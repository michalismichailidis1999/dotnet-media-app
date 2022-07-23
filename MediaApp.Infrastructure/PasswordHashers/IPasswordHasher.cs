namespace MediaApp.Infrastructure.PasswordHashers;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hashed);
}