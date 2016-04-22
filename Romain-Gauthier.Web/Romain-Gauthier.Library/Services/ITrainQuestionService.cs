using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;

namespace Romain_Gauthier.Library.Services
{
    public interface ITrainQuestionService : IDisposable
    {
        void Insert(TrainQuestion trainQuestion);
        void Update();
        TrainQuestion GetTrainQuestion(Guid id);
        IQueryable<TrainQuestion> GetTrainQuestions();
    }
}
