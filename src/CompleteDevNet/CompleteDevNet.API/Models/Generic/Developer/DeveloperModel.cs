﻿using CompleteDevNet.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace CompleteDevNet.API.Models;

public class DeveloperModel
{
    public Guid IdentGuid { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? SkillSet { get; set; }
    public string? Hobby { get; set; }
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