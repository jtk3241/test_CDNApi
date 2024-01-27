using CompleteDevNet.Core.Entities;
using CompleteDevNet.Core.SystemRelated;
using CompleteDevNet.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
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
                SkillSet = x.Skillset
            })
            .AsNoTracking()
            .ToListAsync();
        return results;
    }
}
