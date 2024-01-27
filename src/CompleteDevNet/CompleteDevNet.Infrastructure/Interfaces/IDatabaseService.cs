using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Infrastructure.Interfaces;

public interface IDatabaseService
{
    void OnModelCreating(ModelBuilder modelBuilder);
}
