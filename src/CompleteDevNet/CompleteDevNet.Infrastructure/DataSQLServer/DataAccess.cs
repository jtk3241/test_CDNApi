using CompleteDevNet.Core.Entities;
using CompleteDevNet.Core.SystemRelated;
using CompleteDevNet.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private readonly DatabaseSettings _databaseSettings;

        public DataAccess(
            ILogger logger,
            CDNContext context,
            IOptions<DatabaseSettings> databaseSettingsOptions
            )
        {
            _logger = logger.ForContext<DataAccess>();
            _context = context;
            _databaseSettings = databaseSettingsOptions.Value;
        }

        public virtual async Task<decimal> GetNextIdAsync(string sequence)
        {
            _logger.Debug($"GetNextIdAsync start. sequence:{sequence}.");
            var schema = _databaseSettings.DatabaseSchema;
            if (!string.IsNullOrWhiteSpace(schema))
            {
                schema = $"{schema}.";
            }
            var connection = _context.Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT NEXT VALUE FOR {schema}{sequence};";
                var obj = await cmd.ExecuteScalarAsync();
                var result = Convert.ToDecimal(obj ?? 0);
                connection.Close();
                _logger.Debug($"GetNextIdAsync end. sequence:{sequence}.");
                return result;
            }
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

        public async Task<bool> CheckDeveloperIdentGuid(Guid identGuid)
        {
            var bResult = await _context.TDevelopers
                .AnyAsync(x => x.Identguid == identGuid);
            return bResult;
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

        public async Task<List<DeveloperCore>?> GetDeveloperList(int PageSize = 100, int PageNumber = 0)
        {
            var query = from td in _context.TDevelopers
                        orderby td.Name
                        select td;
            var records = await query
                .Skip(PageNumber * PageSize)
                .Take(PageSize)
                .AsNoTracking()
                .ToListAsync();

            if (records != null && records.Any())
            {
                return records.Select(x => new DeveloperCore()
                {
                    Id = Convert.ToInt64(x.Id),
                    IdentGuid = x.Identguid,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.Phonenumber,
                    Hobby = x.Hobby,
                    SkillSet = x.Skillset,
                    UpdatedOn = x.Updatedon
                }).ToList();
            }
            return null;
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
    }
}
