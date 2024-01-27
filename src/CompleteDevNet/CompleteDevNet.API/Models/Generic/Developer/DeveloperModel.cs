using CompleteDevNet.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace CompleteDevNet.API.Models;

public class DeveloperModel
{
    /// <summary>
    /// Developer identity Guid
    /// </summary>
    public Guid IdentGuid { get; set; }
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
    /// <summary>
    /// Date last updated.
    /// </summary>
    public DateTime? UpdatedOn { get; set; }

    public static DeveloperModel From(DeveloperCore model)
    {
        var objReturn = new DeveloperModel()
        {
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Hobby = model.Hobby,
            IdentGuid = model.IdentGuid,
            Name = model.Name,
            SkillSet = model.SkillSet,
            UpdatedOn = model.UpdatedOn,
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
