using CompleteDevNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Core.Interfaces;

public interface IDeveloperService
{
    Task<List<DeveloperCore>?> GetDeveloperList(int PageSize = 100, int PageNumber = 0);
    Task<DeveloperCore?> RegisterDeveloper(DeveloperCore developer);
    Task<DeveloperCore?> UpdateDeveloper(DeveloperCore developer);
    Task DeleteDeveloper(Guid identGuid);
}
