using CompleteDevNet.Core.Entities;
using CompleteDevNet.Core.Interfaces;
using CompleteDevNet.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Infrastructure;

public class DeveloperService : IDeveloperService
{
    private readonly IDataAccess _dataAccess;
    private readonly ILogger _logger;
    public DeveloperService(ILogger logger, IDataAccess dataAccess) 
    { 
        _dataAccess = dataAccess;
        _logger = logger;
    }
    
    public async Task<List<DeveloperCore>?> GetDeveloperList()
    {
        var objReturn = await _dataAccess.GetDeveloperList();
        //*** extra processing here
        return objReturn;
    }

    public async Task<DeveloperCore?> RegisterDeveloper(DeveloperCore developer)
    {
        _logger.Debug($"RegisterDeveloper start. name:{developer.Name}, email:{developer.Email}.");
        //*** validations
        if (string.IsNullOrWhiteSpace(developer.Name))
        {
            throw new ArgumentException("Developer name cannot be blank.");
        }

        if (string.IsNullOrWhiteSpace(developer.Email))
        {
            throw new ArgumentException("Developer email cannot be blank.");
        }

        if (!CommonUtil.IsValidEmailAddress(developer.Email))
        {
            throw new ArgumentException("Developer email is invalid.");
        }

        bool bEmailExistsInDB = await _dataAccess.CheckDeveloperEmailExists(developer.Email);
        if (bEmailExistsInDB)
        {
            throw new ArgumentException("Developer email already registered.");
        }

        var objReturn = await _dataAccess.AddDeveloper(developer);

        _logger.Debug($"RegisterDeveloper end. name:{developer.Name}, email:{developer.Email}.");
        return objReturn;
    }

    public async Task<DeveloperCore?> UpdateDeveloper(DeveloperCore developer)
    {
        _logger.Debug($"UpdateDeveloper start. name:{developer.Name}, email:{developer.Email}.");
        //*** validations
        bool bIdentValid = await _dataAccess.CheckDeveloperIdentGuid(developer.IdentGuid);
        if (!bIdentValid)
        {
            throw new ArgumentException("Invalid Developer ID.");
        }

        if (string.IsNullOrWhiteSpace(developer.Name))
        {
            throw new ArgumentException("Developer name cannot be blank.");
        }

        if (string.IsNullOrWhiteSpace(developer.Email))
        {
            throw new ArgumentException("Developer email cannot be blank.");
        }

        if (!CommonUtil.IsValidEmailAddress(developer.Email))
        {
            throw new ArgumentException("Developer email is invalid.");
        }

        bool bEmailExistsForOtherUsers = await _dataAccess.CheckDeveloperEmailExists(developer.IdentGuid, developer.Email);
        if (bEmailExistsForOtherUsers)
        {
            throw new ArgumentException("Developer email is already used by another user.");
        }

        var objReturn = await _dataAccess.UpdateDeveloper(developer);

        _logger.Debug($"UpdateDeveloper end. name:{developer.Name}, email:{developer.Email}.");
        return objReturn;
    }
}
