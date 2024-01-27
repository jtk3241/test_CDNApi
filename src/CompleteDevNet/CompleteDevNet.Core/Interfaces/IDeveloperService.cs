using CompleteDevNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Core.Interfaces;

public interface IDeveloperService
{
    Task<List<DeveloperCore>?> GetDeveloperList();
}
