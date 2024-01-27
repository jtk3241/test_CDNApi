using CompleteDevNet.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace CompleteDevNet.API.Models;

public class DeveloperNewModel
{
	/// <summary>
    /// Developer's name.
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;
    /// <summary>
    /// Email address.
    /// </summary>
    [Required]
    public string Email { get; set; } = null!;
    /// <summary>
    /// Phone number. Optional.
    /// </summary>
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// Developer's skillset
    /// </summary>
    public string? SkillSet { get; set; }
    /// <summary>
    /// Developer's hobbies.
    /// </summary>
    public string? Hobby { get; set; }

    public static DeveloperCore ToCore(DeveloperNewModel model)
    {
        var objReturn = new DeveloperCore()
        {
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Hobby = model.Hobby,
            Name = model.Name,
            SkillSet = model.SkillSet
        };
        return objReturn;
    }
}
