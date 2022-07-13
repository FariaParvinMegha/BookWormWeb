using BookWorm.Models;

namespace BookWormWeb.Controllers
{
    internal class ApplicationDbContext
    {
        public IEnumerable<Catagory> Catagories { get; internal set; }
    }
}