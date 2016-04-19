using Romain_Gauthier.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Service.Services
{
    public class TrainArticleService : BaseService, ITrainArticleService
    {
        public TrainArticleService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public TrainArticle GetTrainArticle(Guid id)
        {
            return DbContext.TrainArticles.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<TrainArticle> GetTrainArticles()
        {
            return DbContext.TrainArticles.Where(n => !n.IsDeleted);
        }

        public void Insert(TrainArticle trainArticle)
        {
            DbContext.TrainArticles.Add(trainArticle);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
