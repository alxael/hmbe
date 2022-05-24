using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        T? GetById(object id);

        T? GetFirstOrDefault(Func<T, bool> predicate);

        IEnumerable<T> Get(Func<T, bool> predicate);

        int Count();

        Task InsertAsync(T entity);

        void Update(T entity);
        
        void Delete(T entity);
        
    }
}
