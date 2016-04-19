using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Library.Services
{
   public  interface IProductTypeService : IDisposable
    {
        void Insert(ProductType productType);
        void Update();
        ProductType GetProductType(Guid id);
        IQueryable<ProductType> GetProductTypes();
    }
}
