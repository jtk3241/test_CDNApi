using CompleteDevNet.Core.SystemRelated;
using CompleteDevNet.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Infrastructure.DataOracle;

//Scaffold-DbContext "User ID=TPCDB;Password=TPCDB;Data Source=LOCALHOST:1521/ORCL19C" Oracle.EntityFrameworkCore -OutputDir DataOracle -t T_DEVELOPER -f

public partial class CDNContext : DbContext
{
    private readonly IDatabaseService? _databaseService;
    private readonly DatabaseSettings? _databaseSettings;

    public CDNContext()
    {

    }

    public CDNContext(DbContextOptions<CDNContext> options, IDatabaseService databaseService, IOptions<DatabaseSettings> databaseSettings)
        : base(options)
    {
        _databaseService = databaseService;
        _databaseSettings = databaseSettings.Value;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseOracle("Data Source=x:9/x");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (_databaseService != null)
            _databaseService.OnModelCreating(modelBuilder);
        if (_databaseSettings != null)
            OracleConfiguration.TnsAdmin = _databaseSettings.TnsAdmin;
    }

    //*** add all dbsets below here
    public virtual DbSet<TDeveloper> TDevelopers { get; set; }

}
