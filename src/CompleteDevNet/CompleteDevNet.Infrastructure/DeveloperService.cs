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
}
