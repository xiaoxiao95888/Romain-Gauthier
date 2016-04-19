using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Library.Services
{
    public interface IDataContext : IObjectContextAdapter, IDisposable
    {
        IDbSet<News> Newses { get; set; }
        IDbSet<NewsType> NewsTypes { get; set; }
        IDbSet<Personnel> Personnels { get; set; }
        IDbSet<TrainArticle> TrainArticles { get; set; }
        IDbSet<TrainQuestion> TrainQuestions { get; set; }
        IDbSet<TrainAnswer> TrainAnswers { get; set; }
        IDbSet<TrainType> TrainTypes { get; set; }
        IDbSet<ViewRecord> ViewRecords { get; set; }
        IDbSet<ProductType> ProductTypes { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<File> Files { get; set; }
        int SaveChanges();
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
