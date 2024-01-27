using CompleteDevNet.Core.Entities;

namespace CompleteDevNet.API.Models;

public class UserModel
{
    public Guid IdentGuid { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? SkillSet { get; set; }
    public string? Hobby { get; set; }

    public static UserModel From(UserCore model)
    {
        var objReturn = new UserModel()
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

    public static UserCore ToCore(UserModel model)
    {
        var objReturn = new UserCore()
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
