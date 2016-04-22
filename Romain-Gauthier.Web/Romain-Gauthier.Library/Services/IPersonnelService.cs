using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Library.Services
{
    public interface IPersonnelService : IDisposable
    {
        void Insert(Personnel personnel);
        void Update();
        Personnel GetPersonnel(Guid id);
        Personnel GetPersonnel(string id);
        IQueryable<Personnel> GetPersonnels();
        IQueryable<string> GetLicense();
        IQueryable<PersonnelGroup> GetPersonnelGroups();
        IQueryable<TrainAnswer> GetTrainAnswers();
    }
}
