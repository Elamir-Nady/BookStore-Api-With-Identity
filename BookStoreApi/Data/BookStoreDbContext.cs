using BookStoreApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Data
{
    public class BookStoreDbContext:IdentityDbContext<User>
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options)
        {

        }
        public DbSet<Auther> Authers { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
