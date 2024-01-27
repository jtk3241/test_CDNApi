using CompleteDevNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Infrastructure.Interfaces;

public interface IDataAccess
{
    Task<List<DeveloperCore>?> GetDeveloperList();
    Task<bool> CheckDeveloperEmailExists(string email);
    Task<DeveloperCore> AddDeveloper(DeveloperCore developer);
}
