using Microsoft.EntityFrameworkCore;

namespace BookStore.Model
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
           : base(options)
        {
        }

        public DbSet<BookModel> books { get; set; }
    }
}
