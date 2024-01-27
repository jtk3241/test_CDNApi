namespace CompleteDevNet.API.Models.SystemRelated;

public class UserTokenModel
{
    public string Token { get; set; } = string.Empty;
    public Guid GuidId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public TimeSpan Validaty { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiredTime { get; set; }
}
