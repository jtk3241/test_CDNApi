using CompleteDevNet.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace CompleteDevNet.API.Models;

public class DeveloperNewModel
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? SkillSet { get; set; }
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
