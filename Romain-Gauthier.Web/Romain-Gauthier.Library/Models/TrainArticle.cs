using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models.Interfaces;

namespace Romain_Gauthier.Library.Models
{
    public class TrainArticle : IDtStamped
    {
        public Guid Id { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Personnel> Personnels { get; set; }
        public virtual ICollection<TrainQuestion> TrainQuestions { get; set; }
        /// <summary>
        /// 浏览记录
        /// </summary>
        public virtual ICollection<ViewRecord> ViewRecords { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
