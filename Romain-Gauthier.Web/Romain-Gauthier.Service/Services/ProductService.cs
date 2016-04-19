using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;

namespace Romain_Gauthier.Service.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public Product GetProduct(Guid id)
        {
            return DbContext.Products.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Product> GetProducts()
        {
            return DbContext.Products.Where(n => !n.IsDeleted);
        }

        public void Insert(Product product)
        {
            DbContext.Products.Add(product);
            Update();
            
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
