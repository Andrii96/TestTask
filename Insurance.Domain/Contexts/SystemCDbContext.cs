using Insurance.Domain.Entities.SystemC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Contexts
{
    public class SystemCDbContext:DbContext
    {
        public SystemCDbContext() : base("SystemCDbConnectionString") { }

        public DbSet<Insurer> Insurer { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicy { get; set; }
        public DbSet<Beneficiary> Beneficiary { get; set; }
        public DbSet<Agent> Agent { get; set; }
    }
}
