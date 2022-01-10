using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApi.Repostory
{
    public interface IGenericRepostory<T>
    {
        IEnumerable<T> Get();
        Task<T> GetByID(int id);
        Task<int> Add(T entity);
        void Update(T entity);
        Task Remove(int id);
        void RemoveRange(IEnumerable<T> ts);

    }
}
