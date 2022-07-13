using BookWorm.Models;
using Microsoft.EntityFrameworkCore;

namespace BookWorm.DataAccess
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Catagory> Catagories { get; set; }
    }
}
