namespace MediaApp.Infrastructure.Auth;

public class JwtAuthenticationHandler : IAuthenticationHandler
{
    private readonly JwtSettings _jwtSettings;

    public JwtAuthenticationHandler(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string CreateAccessToken(User user)
    {
        var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Username)
            };

        var token = new JwtSecurityToken
        (
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audiences[0],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}