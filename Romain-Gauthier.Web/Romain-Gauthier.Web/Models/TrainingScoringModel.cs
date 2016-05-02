using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models
{
    public class TrainingScoringDetailModel
    {
        public PersonnelModel PersonnelModel { get; set; }
        public Guid TrainArticleId { get; set; }
        public string TrainArticleTitle { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        //得分
        public decimal Score { get; set; }
        public bool IsCorrect { get; set; }   
    }
   
}