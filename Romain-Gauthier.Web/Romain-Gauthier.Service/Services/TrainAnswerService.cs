using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;

namespace Romain_Gauthier.Service.Services
{
    public class TrainAnswerService : BaseService, ITrainAnswerService
    {
        public TrainAnswerService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public TrainAnswer GetTrainAnswer(Guid id)
        {
            return DbContext.TrainAnswers.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<TrainAnswer> GetTrainAnswers()
        {
            return DbContext.TrainAnswers.Where(n => !n.IsDeleted);
        }

        public void Insert(TrainAnswer trainAnswer)
        {
            DbContext.TrainAnswers.Add(trainAnswer);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
