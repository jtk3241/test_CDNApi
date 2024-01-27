using CompleteDevNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Infrastructure.Interfaces;

public interface IDataAccess
{
    Task<List<DeveloperCore>?> GetDeveloperList(int PageSize = 100, int PageNumber = 0);
    Task<bool> CheckDeveloperIdentGuid(Guid identGuid);
    Task<bool> CheckDeveloperEmailExists(string email);
    Task<bool> CheckDeveloperEmailExists(Guid identGuid, string email);
    Task<DeveloperCore> AddDeveloper(DeveloperCore developer);
    Task<DeveloperCore> UpdateDeveloper(DeveloperCore developer);
    Task DeleteDeveloper(Guid identGuid);
}
