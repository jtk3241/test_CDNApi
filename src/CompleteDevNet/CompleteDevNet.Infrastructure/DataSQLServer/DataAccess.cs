using CompleteDevNet.Core.Entities;
using CompleteDevNet.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Infrastructure.DataSQLServer
{
    public class DataAccess : IDataAccess
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

        public Task<DeveloperCore> AddDeveloper(DeveloperCore developer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckDeveloperEmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckDeveloperEmailExists(Guid identGuid, string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckDeveloperIdentGuid(Guid identGuid)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDeveloper(Guid identGuid)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeveloperCore>?> GetDeveloperList(int PageSize = 100, int PageNumber = 0)
        {
            throw new NotImplementedException();
        }

        public Task<DeveloperCore> UpdateDeveloper(DeveloperCore developer)
        {
            throw new NotImplementedException();
        }
    }
}
