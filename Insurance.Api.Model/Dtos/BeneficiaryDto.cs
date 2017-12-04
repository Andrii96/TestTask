using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Api.Model.Dtos
{
    public class BeneficiaryDto : Interfaces.IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
