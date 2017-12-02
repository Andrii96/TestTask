using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Entities.SystemC
{
    public class InsurancePolicy:IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public long Number { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTill { get; set; }

        [ForeignKey("Insurer")]
        public Guid InsurerId { get; set; }

        [ForeignKey("Agent")]
        public Guid AgentId { get; set; }

        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
        public virtual Insurer Insurer { get; set; }
        public virtual Agent Agent { get; set; }

    }
}
