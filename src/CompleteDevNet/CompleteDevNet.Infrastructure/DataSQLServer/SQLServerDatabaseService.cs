using CompleteDevNet.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Infrastructure.DataSQLServer
{
    public class SQLServerDatabaseService : IDatabaseService
    {
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TDeveloper>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_DEVELOPER");

                entity.ToTable("T_DEVELOPER");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ID");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");
                entity.Property(e => e.Hobby)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("HOBBY");
                entity.Property(e => e.Identguid).HasColumnName("IDENTGUID");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");
                entity.Property(e => e.Skillset)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("SKILLSET");
                entity.Property(e => e.Updatedon).HasColumnName("UPDATEDON");
            });
            modelBuilder.HasSequence<decimal>("S_DEVELOPER")
                .HasMin(1L)
                .HasMax(899999999999999L);
        }
    }
}
