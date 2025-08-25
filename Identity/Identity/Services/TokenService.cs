using Identity.Interfaces;
using Identity.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Identity.Services;

public class TokenService : ITokenService
{
    private const string TokenSecret = "ThisShouldBeLoadedByThePipelineAndStoredInAwsSecretsManager";
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromMinutes(15);

    public string GenerateToken(TokenGenerationRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(TokenSecret);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, request.Email),
            new(JwtRegisteredClaimNames.Email, request.Email),
            new("userid", request.UserId.ToString())
        };

        foreach (var claimPair in request.CustomClaims)
        {
            var jsonElement = claimPair.Value;
            var valueType = jsonElement.GetValueKind()! switch
            {
                JsonValueKind.True => ClaimValueTypes.Boolean,
                JsonValueKind.False => ClaimValueTypes.Boolean,
                JsonValueKind.Number => ClaimValueTypes.Double,
                _ => ClaimValueTypes.String
            };

            var claim = new Claim(claimPair.Key, claimPair.Value?.ToString()!, valueType);
            claims.Add(claim);
        }

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifetime),
            Issuer = "https://id.bytesizebrews.com",
            Audience = "https://audience.bytesizebrews.com",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}