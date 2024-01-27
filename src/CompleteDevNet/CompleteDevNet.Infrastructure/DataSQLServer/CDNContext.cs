using System;
using System.Collections.Generic;
using CompleteDevNet.Core.SystemRelated;
using CompleteDevNet.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CompleteDevNet.Infrastructure.DataSQLServer;

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
            optionsBuilder.UseSqlServer("Server=dummyconnectionstring");
        }
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (_databaseService != null)
            _databaseService.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    //*** add all dbsets below here
    public virtual DbSet<TDeveloper> TDevelopers { get; set; }
}
