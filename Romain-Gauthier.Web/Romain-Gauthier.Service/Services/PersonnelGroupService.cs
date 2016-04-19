using Romain_Gauthier.Library.Services;
using System;
using System.Linq;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Service.Services
{
    public class PersonnelGroupService : BaseService, IPersonnelGroupService
    {
        public PersonnelGroupService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public PersonnelGroup GetPersonnelGroup(Guid id)
        {
            return DbContext.PersonnelGroups.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<PersonnelGroup> GetPersonnelGroups()
        {
            return DbContext.PersonnelGroups.Where(n => !n.IsDeleted);
        }

        public IQueryable<ProductType> GetProductTypes()
        {
            return DbContext.ProductTypes.Where(n => !n.IsDeleted);
        }

        public IQueryable<TrainArticle> GetTrainArticles()
        {
            return DbContext.TrainArticles.Where(n => !n.IsDeleted);
        }

        public void Insert(PersonnelGroup personnelGroup)
        {
            DbContext.PersonnelGroups.Add(personnelGroup);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
