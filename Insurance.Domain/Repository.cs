using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain
{
    public class Repository<R,T> : IRepository<T> where T : class where R:DbContext, new()
    {
        #region Fields

        private readonly R _dbContext = new R();
        private DbSet<T> _dbSet;

        #endregion

        #region Properties
         DbSet<T> Entities
         {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = _dbContext.Set<T>();
                }

                return _dbSet;
            }
         }

        #endregion

        #region Methods

        public IEnumerable<T> Get()
        {
            return Entities.AsNoTracking().ToList();
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return Entities.AsNoTracking().Where(predicate);
        }

        public T GetById(Guid id)
        {
            return Entities.Find(id);
        }

        #endregion
    }
}
