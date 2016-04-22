using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models
{
    public class TrainQuestionModel
    {
        public Guid Id { get; set; }
        public Guid TrainArticleId { get; set; }
        public string TrainArticleName { get; set; }
        public string Question { get; set; }
        public decimal Score { get; set; }
        public TrainAnswerModel[] TrainAnswerModels { get; set; }
    }
}