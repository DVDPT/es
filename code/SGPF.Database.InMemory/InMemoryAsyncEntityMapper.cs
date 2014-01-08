using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.Database.InMemory
{
    class InMemoryAsyncEntityMapper<TKey, TEntity> : List<TEntity>, IAsyncEntityMapper<TKey, TEntity>
    {
        private readonly Func<TEntity, TKey> _entityKeyExtractor;



        public InMemoryAsyncEntityMapper(Func<TEntity, TKey> entityKeyExtractor)
        {
            _entityKeyExtractor = entityKeyExtractor;
        }


        public async Task<TEntity> Get(TKey key)
        {
            return this.FirstOrDefault(e => _entityKeyExtractor(e).Equals(key));
        }

        public async Task<TKey> Add(TEntity obj)
        {
            base.Add(obj);
            return _entityKeyExtractor(obj);
        }

        public async Task Delete(TEntity obj)
        {
            Remove(obj);

        }

        public async Task<IEnumerable<TEntity>> All()
        {
            return this;
        }

        public async Task Update(TEntity value)
        {
        }
    }
}
