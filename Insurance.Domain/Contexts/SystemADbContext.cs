using Insurance.Domain.Entities.SystemA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Contexts
{
    public class SystemADbContext:DbContext
    {
        public SystemADbContext() : base("SystemADb") { }

        public DbSet<Insurer> Insurer { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicy { get; set; }
        public DbSet<Beneficiary> Beneficiary { get; set; }
    }
}
