using Insurance.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DAL.Entities.SystemA
{
    public class InsurancePolicy:Entity
    {
        public long Number { get; set; }
        public bool IsActive { get; set; }
        public string AgentName { get; set; }
        public int InsurerId { get; set; }
        public int BeneficiaryId { get; set; }
    }
}
