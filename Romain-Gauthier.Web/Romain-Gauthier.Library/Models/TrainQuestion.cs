using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models.Interfaces;

namespace Romain_Gauthier.Library.Models
{
    public class TrainQuestion : IDtStamped
    {
        public Guid Id { get; set; }
        public Guid TrainArticleId { get; set; }
        [ForeignKey("TrainArticleId")]
        public virtual TrainArticle TrainArticle { get; set; }
        public string Question { get; set; }
        public virtual ICollection<TrainAnswer> TrainAnswers { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        public decimal Score { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class TrainAnswer : IDtStamped
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public Guid TrainQuestionId { get; set; }
        [ForeignKey("TrainQuestionId")]
        public virtual TrainQuestion TrainQuestion { get; set; }
        public virtual ICollection<Personnel> Personnels { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
