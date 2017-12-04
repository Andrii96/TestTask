using Insurance.Domain.Entities.SystemB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Contexts
{
    public class SystemBDbContext:DbContext
    {
        public SystemBDbContext() : base("SystemBDb") { }

        public DbSet<Insurer> Insurer { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicy { get; set; }
        public DbSet<Agent> Agent { get; set; }
    }
}
