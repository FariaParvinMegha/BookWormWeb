using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWorm.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICatagoryRepository Catagory { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }

        void Save();
    }
}
