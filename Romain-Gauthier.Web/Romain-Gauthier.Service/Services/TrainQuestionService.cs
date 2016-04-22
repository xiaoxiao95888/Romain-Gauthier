using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;

namespace Romain_Gauthier.Service.Services
{
    public class TrainQuestionService : BaseService, ITrainQuestionService
    {
        public TrainQuestionService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public TrainQuestion GetTrainQuestion(Guid id)
        {
            return DbContext.TrainQuestions.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<TrainQuestion> GetTrainQuestions()
        {
            return DbContext.TrainQuestions.Where(n => !n.IsDeleted);
        }

        public void Insert(TrainQuestion trainQuestion)
        {
            DbContext.TrainQuestions.Add(trainQuestion);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
