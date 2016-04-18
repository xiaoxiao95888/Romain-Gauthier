using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;

namespace Romain_Gauthier.Service.Services
{
    public class NewsService : BaseService, INewsService
    {
        public NewsService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public News GetNews(Guid id)
        {
            return DbContext.Newses.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<News> GetNewses()
        {
            return DbContext.Newses.Where(n => !n.IsDeleted);
        }

        public void Insert(News news)
        {
            DbContext.Newses.Add(news);
            Update();
            
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
