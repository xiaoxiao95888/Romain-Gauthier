using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Library.Services
{
    public interface ITrainAnswerService : IDisposable
    {
        void Insert(TrainAnswer trainAnswer);
        void Update();
        TrainAnswer GetTrainAnswer(Guid id);
        IQueryable<TrainAnswer> GetTrainAnswers();
    }
}
