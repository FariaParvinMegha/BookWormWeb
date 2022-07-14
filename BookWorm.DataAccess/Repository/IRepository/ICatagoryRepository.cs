using BookWorm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWorm.DataAccess.Repository.IRepository
{
    public interface ICatagoryRepository : IRepository<Catagory>
    {
        void Update(Catagory obj);
    }
}
 