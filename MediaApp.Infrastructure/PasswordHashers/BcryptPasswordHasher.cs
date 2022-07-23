namespace MediaApp.Infrastructure.PasswordHashers;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hashed)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashed);
    }
}