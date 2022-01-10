using BookStoreApi.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApi.Repostory
{
    public class GenericRepostory<T> : IGenericRepostory<T> where T : BaseModel
    {
        BookStoreDbContext Context;
        DbSet<T> Table;
        public GenericRepostory(BookStoreDbContext context)
        {
            Context = context;
            Table = Context.Set<T>();
        }

        public   IEnumerable<T> Get()
        {
           return  Table;
        }

        public async Task< T> GetByID(int id)
        {
            return await  Table.FindAsync(id);
        }

        public async Task<int> Add(T entity)
        {
            await Table.AddAsync(entity);
            return entity.ID;
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public async Task Remove(int id)
        {
            var entity = await GetByID(id);
            Table.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> ts)
        {
            Table.RemoveRange(ts);
        }
    }
}
