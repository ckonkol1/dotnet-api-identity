using Identity.Models;

namespace Identity.Interfaces;

public interface ITokenService
{
    public string GenerateToken(TokenGenerationRequest request);
}