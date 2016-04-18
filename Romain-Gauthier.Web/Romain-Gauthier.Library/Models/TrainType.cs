using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models.Interfaces;

namespace Romain_Gauthier.Library.Models
{
    public class TrainType: IDtStamped
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        /// <summary>
        /// 培训的文章
        /// </summary>
        public virtual ICollection<TrainArticle> TrainArticles { get; set; }
        public virtual ICollection<TrainQuestion> TrainQuestions { get; set; } 
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
