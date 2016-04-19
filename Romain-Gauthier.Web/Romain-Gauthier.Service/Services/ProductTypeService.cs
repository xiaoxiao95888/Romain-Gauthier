using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;

namespace Romain_Gauthier.Service.Services
{
    public class ProductTypeService : BaseService, IProductTypeService
    {
        public ProductTypeService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public void Insert(ProductType productType)
        {
            DbContext.ProductTypes.Add(productType);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public ProductType GetProductType(Guid id)
        {
            return DbContext.ProductTypes.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<ProductType> GetProductTypes()
        {
            return DbContext.ProductTypes.Where(n => !n.IsDeleted);
        }

        public IQueryable<Personnel> GetPersonnels()
        {
            return DbContext.Personnels.Where(n => !n.IsDeleted);
        }
    }
}
