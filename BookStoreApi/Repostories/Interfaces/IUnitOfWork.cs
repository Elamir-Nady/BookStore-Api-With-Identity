
using BookStoreApi.Models;
using BookStoreApi.Repostories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApi.Repostory
{
   public interface IUnitOfWork
    {
        IGenericRepostory<Auther> GeAutherRepo();
        IGenericRepostory<Book> GetBookRepo();
        IUserRepostory GetUserRepo();
        Task Save();
    }
}
