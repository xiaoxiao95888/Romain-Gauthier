using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Models.Interfaces;
using Romain_Gauthier.Library.Services;
using System.Data.Entity;

namespace Romain_Gauthier.Test
{
   public  class RomainGauthierDataContext : DbContext, IDataContext
    {
        public IDbSet<News> Newses { get; set; }
        public IDbSet<NewsType> NewsTypes { get; set; }
        public IDbSet<Personnel> Personnels { get; set; }
        public IDbSet<TrainArticle> TrainArticles { get; set; }
        public IDbSet<TrainQuestion> TrainQuestions { get; set; }
        public IDbSet<TrainAnswer> TrainAnswers { get; set; }
        public IDbSet<TrainType> TrainTypes { get; set; }
        public IDbSet<ViewRecord> ViewRecords { get; set; }
        public IDbSet<ProductType> ProductTypes { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<File> Files { get; set; }
        IDbSet<TEntity> IDataContext.Set<TEntity>()
        {
            return this.Set<TEntity>();
        }
        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries<IDtStamped>();

            foreach (var dtStamped in entities)
            {
                if (dtStamped.State == EntityState.Added)
                {
                    dtStamped.Entity.CreatedTime = DateTime.Now;
                    dtStamped.Entity.UpdateTime = DateTime.Now;
                }

                if (dtStamped.State == EntityState.Modified)
                {
                    dtStamped.Entity.UpdateTime = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new CategoryMapping());
            //modelBuilder.Configurations.Add(new AvatarMapping());
            //modelBuilder.Configurations.Add(new LetterMapping());
            //modelBuilder.Configurations.Add(new RetailerMapping());
            //modelBuilder.Configurations.Add(new SiteMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
