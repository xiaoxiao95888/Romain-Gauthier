using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Library.Services
{
    public interface IProductService : IDisposable
    {
        void Insert(Product product);
        void Update();
        Product GetProduct(Guid id);
        IQueryable<Product> GetProducts();
    }
}
