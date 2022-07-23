namespace MediaApp.Api.Registers.Builder;

public class AuthorizationRegister : IWebApplicationBuilderRegister
{
    public void Register(WebApplicationBuilder builder)
    {
        var jwtSettings = new JwtSettings();
        builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudiences = jwtSettings.Audiences,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
                jwt.Audience = jwtSettings.Audiences[0];
                jwt.ClaimsIssuer = jwtSettings.Issuer;
            });

        builder.Services.AddTransient<IAuthenticationHandler, JwtAuthenticationHandler>(_options =>
        {
            return new JwtAuthenticationHandler(jwtSettings);
        });
    }
}
