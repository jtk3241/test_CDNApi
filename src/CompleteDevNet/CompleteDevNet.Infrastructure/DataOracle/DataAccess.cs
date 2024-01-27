using CompleteDevNet.Core.Entities;
using CompleteDevNet.Core.SystemRelated;
using CompleteDevNet.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Infrastructure.DataOracle;

public partial class DataAccess : IDataAccess
{
    private readonly CDNContext _context;
    private readonly ILogger _logger;

    public DataAccess(
        ILogger logger,
        CDNContext context
        )
    {
        _logger = logger.ForContext<DataAccess>();
        _context = context;
    }

    public virtual async Task<decimal> GetNextIdAsync(string sequence)
    {
        _logger.Debug($"GetNextIdAsync start. sequence:{sequence}.");
        using (var cmd = _context.Database.GetDbConnection().CreateCommand())
        {
            if (cmd.Connection?.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.CommandText = $"select {sequence}.nextval from dual";
            var obj = await cmd.ExecuteScalarAsync();
            var result = Convert.ToDecimal(obj ?? 0);
            _logger.Debug($"GetNextIdAsync end. sequence:{sequence}.");
            return result;
        }
    }

    public async Task<List<DeveloperCore>?> GetDeveloperList()
    {
        var results = await _context.TDevelopers
            .Select(x => new DeveloperCore
            {
                Id = Convert.ToInt64(x.Id),
                IdentGuid = x.Identguid,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.Phonenumber,
                Hobby = x.Hobby,
                SkillSet = x.Skillset,
                UpdatedOn = x.Updatedon
            })
            .AsNoTracking()
            .ToListAsync();
        return results;
    }

    public async Task<bool> CheckDeveloperIdentGuid(Guid identGuid)
    {
        var bResult = await _context.TDevelopers
            .AnyAsync(x => x.Identguid == identGuid);
        return bResult;
    }

    public async Task<bool> CheckDeveloperEmailExists(string email)
    {
        var bResult = await _context.TDevelopers
            .Where(x => x.Email.ToLower() == email.ToLower())
            .AnyAsync();
        return bResult;
    }

    public async Task<bool> CheckDeveloperEmailExists(Guid identGuid, string email)
    {
        var bResult = await _context.TDevelopers
            .Where(x =>
                x.Identguid != identGuid
                && x.Email.ToLower() == email.ToLower()
            )
            .AnyAsync();
        return bResult;
    }

    public async Task<DeveloperCore> AddDeveloper(DeveloperCore developer)
    {
        developer.IdentGuid = Guid.NewGuid();
        developer.Id = Convert.ToInt64(await GetNextIdAsync("s_developer"));

        TDeveloper objNewDev = new TDeveloper()
        {
            Id = developer.Id,
            Identguid = developer.IdentGuid,
            Name = developer.Name,
            Email = developer.Email,
            Hobby = developer.Hobby,
            Phonenumber = developer.PhoneNumber,
            Skillset = developer.SkillSet,
            Updatedon = DateTime.Now
        };
        await _context.TDevelopers.AddAsync(objNewDev);
        await _context.SaveChangesAsync();

        developer.UpdatedOn = objNewDev.Updatedon;

        return developer;
    }

    public async Task<DeveloperCore> UpdateDeveloper(DeveloperCore developer)
    {
        var objDev = await _context.TDevelopers.FirstOrDefaultAsync(x => x.Identguid == developer.IdentGuid);
        if (objDev == null) 
        {
            throw new ArgumentException($"Developer record not found. IdentGuid:{developer.IdentGuid}");
        }

        //*** update fields
        if (!string.Equals(objDev.Name, developer.Name)
            || string.Equals(objDev.Email, developer.Email)
            || string.Equals(objDev.Phonenumber, developer.PhoneNumber)
            || string.Equals(objDev.Hobby, developer.Hobby)
            || string.Equals(objDev.Skillset, developer.SkillSet)
            )
        {
            objDev.Name = developer.Name;
            objDev.Email = developer.Email;
            objDev.Phonenumber = developer.PhoneNumber;
            objDev.Hobby = developer.Hobby;
            objDev.Skillset = developer.SkillSet;
            objDev.Updatedon = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        developer.UpdatedOn = objDev.Updatedon;
        return developer;
    }

    public async Task DeleteDeveloper(Guid identGuid)
    {
        var objDev = await _context.TDevelopers.FirstOrDefaultAsync(x => x.Identguid == identGuid);
        if (objDev == null)
        {
            throw new ArgumentException($"Developer record not found. IdentGuid:{identGuid}");
        }

        _context.TDevelopers.Remove(objDev);
        await _context.SaveChangesAsync();
    }
}
