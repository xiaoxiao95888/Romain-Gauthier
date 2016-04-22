using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models
{
    public class TrainAnswerModel
    {
        public Guid Id { get; set; }
        public Guid TrainQuestionId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}