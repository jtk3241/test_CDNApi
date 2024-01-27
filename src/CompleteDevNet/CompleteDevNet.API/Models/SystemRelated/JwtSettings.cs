using System.ComponentModel.DataAnnotations;

namespace CompleteDevNet.API;

public class JwtSettings
{
    public bool ValidateIssuerSigningKey { get; set; }
    [MaxLength]
    public string IssuerSigningKey { get; set; } = string.Empty;
    public bool ValidateIssuer { get; set; } = true;
    [MaxLength]
    public string ValidIssuer { get; set; } = string.Empty;
    public bool ValidateAudience { get; set; } = true;
    [MaxLength]
    public string ValidAudience { get; set; } = string.Empty;
    public bool RequireExpirationTime { get; set; }
    public bool ValidateLifetime { get; set; } = true;
}
