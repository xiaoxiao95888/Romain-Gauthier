using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models.Interfaces;

namespace Romain_Gauthier.Library.Models
{
    public class TrainQuestion : IDtStamped
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public virtual ICollection<TrainAnswer> TrainAnswers { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class TrainAnswer
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public virtual TrainQuestion TrainQuestion { get; set; }
        public bool IsCorrect { get; set; }
    }
}
