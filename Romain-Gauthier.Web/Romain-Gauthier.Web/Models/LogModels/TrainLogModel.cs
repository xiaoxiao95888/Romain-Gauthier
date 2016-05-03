using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models.LogModels
{
    public class TrainLogModel
    {
        public Guid TrainArticleId { get; set; }
        public string TrainArticleTitle { get; set; }
        public Guid PersonnelId { get; set; }
        public string PersonnelName { get; set; }
        public DateTime Date { get; set; }
    }
}