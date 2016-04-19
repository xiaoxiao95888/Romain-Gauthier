using Romain_Gauthier.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romain_Gauthier.Library.Services
{
    public interface ITrainArticleService : IDisposable
    {
        void Insert(TrainArticle trainArticle);
        void Update();
        TrainArticle GetTrainArticle(Guid id);
        IQueryable<TrainArticle> GetTrainArticles();
    }
}
