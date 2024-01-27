namespace CompleteDevNet.API.Models;

public class UserTokenModel
{
    /// <summary>
    /// Json Web Token return value
    /// </summary>
    public string Token { get; set; } = string.Empty;
    /// <summary>
    /// Unique request ID.
    /// </summary>
    public Guid GuidId { get; set; }
    /// <summary>
    /// User's name for the generated JWT string.
    /// </summary>
    public string UserName { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public TimeSpan Validaty { get; set; }
    /// <summary>
    /// URL to get refresh token
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
    /// <summary>
    /// Token's expiry date/time.
    /// </summary>
    public DateTime ExpiredTime { get; set; }
}
