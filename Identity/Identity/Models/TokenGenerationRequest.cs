using System.Text.Json.Nodes;

namespace Identity.Models;

public class TokenGenerationRequest
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;

    public Dictionary<string, JsonNode?> CustomClaims { get; set; } = new();
}