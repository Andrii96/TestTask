using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain
{
    public interface IRepository<T> where T:class
    {
         IEnumerable<T> Get();
         IEnumerable<T> Get(Func<T, bool> predicate);
         T GetById(Guid id);
    }
}
