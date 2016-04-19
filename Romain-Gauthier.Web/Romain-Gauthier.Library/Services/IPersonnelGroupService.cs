using Romain_Gauthier.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romain_Gauthier.Library.Services
{
    public interface IPersonnelGroupService : IDisposable
    {
        void Insert(PersonnelGroup personnelGroup);
        void Update();
        PersonnelGroup GetPersonnelGroup(Guid id);
        IQueryable<PersonnelGroup> GetPersonnelGroups();
        IQueryable<TrainArticle> GetTrainArticles();
        IQueryable<ProductType> GetProductTypes();
    }
}
