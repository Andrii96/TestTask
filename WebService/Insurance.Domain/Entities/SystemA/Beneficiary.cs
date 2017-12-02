using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Entities.SystemA
{
    public class Beneficiary : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("InsurancePolicy")]
        public Guid InsurancePolicyId { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
