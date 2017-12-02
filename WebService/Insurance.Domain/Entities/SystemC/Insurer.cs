using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Entities.SystemC
{
    public class Insurer:IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
