using Romain_Gauthier.Library.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romain_Gauthier.Library.Models
{
    public class PersonnelGroup : IDtStamped
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Personnel> Personnels { get; set; }
        public virtual ICollection<TrainArticle> TrainArticles { get; set; }
        public virtual ICollection<ProductType> ProductTypes { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
