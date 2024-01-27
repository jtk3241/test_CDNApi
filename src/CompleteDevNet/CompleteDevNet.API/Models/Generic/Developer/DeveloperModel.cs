using CompleteDevNet.Core.Entities;

namespace CompleteDevNet.API.Models;

public class DeveloperModel
{
    public Guid IdentGuid { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? SkillSet { get; set; }
    public string? Hobby { get; set; }

    public static DeveloperModel From(DeveloperCore model)
    {
        var objReturn = new DeveloperModel()
        {
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Hobby = model.Hobby,
            IdentGuid = model.IdentGuid,
            Name = model.Name,
            SkillSet = model.SkillSet
        };
        return objReturn;
    }

    public static DeveloperCore ToCore(DeveloperModel model)
    {
        var objReturn = new DeveloperCore()
        {
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Hobby = model.Hobby,
            IdentGuid = model.IdentGuid,
            Name = model.Name,
            SkillSet = model.SkillSet
        };
        return objReturn;
    }
}
