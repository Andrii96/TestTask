using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Entities.SystemA
{
    public class InsurancePolicy : IEntity
    {
        [Key]
        public Guid Id { get; set ; }

        [Required]
        public long Number { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string AgentModel { get; set; }

        [ForeignKey("Insurer")]
        public Guid InsurerId { get; set; }
       
        public Insurer Insurer { get; set; }
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
       
    }
}
