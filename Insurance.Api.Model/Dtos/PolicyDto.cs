using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Api.Model.Dtos
{
    public class PolicyDto //: Interfaces.IDto
    {
        //public Guid Id { get; set; }
        public long Number { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
        public InsurerDto Insurer { get; set; }
        public AgentDto Agent { get; set; }
        public IEnumerable<BeneficiaryDto> Beneficiary { get; set; }
    }
}