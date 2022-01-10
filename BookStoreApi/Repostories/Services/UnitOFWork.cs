
using BookStoreApi.Data;
using BookStoreApi.Models;
using BookStoreApi.Repostories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApi.Repostory
{
   public class UnitOFWork:IUnitOfWork
    {
        public BookStoreDbContext Context { get; }
        public IGenericRepostory<Auther> AutherRepo { get; }
        public IGenericRepostory<Book> BookRepo { get; }
        public IUserRepostory UserRepo { get; }

        public UnitOFWork(
                          IGenericRepostory<Auther> AutherRepo,
                          IGenericRepostory<Book> BookRepo,
                          IUserRepostory UserRepo,
                          BookStoreDbContext context)
        {
            Context = context;
            this.AutherRepo = AutherRepo;
            this.BookRepo = BookRepo;
            this.UserRepo = UserRepo;
        }



        public IGenericRepostory<Auther> GeAutherRepo()
        {
            return AutherRepo;
        }

        public IGenericRepostory<Book> GetBookRepo()
        {
           return BookRepo;
        }
        public IUserRepostory GetUserRepo()
        {
            return UserRepo;
        }
        public async Task Save()
        {
          await Context.SaveChangesAsync();
        }

        
    }
}
