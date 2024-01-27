using System.ComponentModel.DataAnnotations;

namespace CompleteDevNet.API.Models;

public class UserLoginModel
{
    /// <summary>
    /// User login name
    /// </summary>
    [Required]
    public string UserName { get; set; } = string.Empty;
    /// <summary>
    /// Password for user
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}
