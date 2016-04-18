using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;

namespace Romain_Gauthier.Service.Services
{
    public class NewsTypeService : BaseService, INewsTypeService
    {
        public NewsTypeService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public NewsType GetNewsType(Guid id)
        {
            return DbContext.NewsTypes.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<NewsType> GetNewsTypes()
        {
            return DbContext.NewsTypes.Where(n => !n.IsDeleted);
        }

        public void Insert(NewsType newsType)
        {
            DbContext.NewsTypes.Add(newsType);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
