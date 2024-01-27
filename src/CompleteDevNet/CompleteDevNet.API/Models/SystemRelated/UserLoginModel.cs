using System.ComponentModel.DataAnnotations;

namespace CompleteDevNet.API.Models.SystemRelated;

public class UserLoginModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
