using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.Database
{
    public interface IAsyncEntityMapper<TKey, TEntity>
    {
        Task<TEntity> Get(TKey key);
        Task<TKey> Add(TEntity obj);
        Task Delete(TEntity obj);
        Task<IEnumerable<TEntity>> All();
        Task Update(TEntity value);
    }
}
