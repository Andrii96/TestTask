using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DAL.Abstract
{
    public abstract class Entity
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
