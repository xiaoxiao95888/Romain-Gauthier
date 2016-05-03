using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Romain_Gauthier.Library.Models
{
    public class ViewRecord : IDtStamped
    {
        public Guid Id { get; set; }
        public Guid? PersonnelId { get; set; }
        [ForeignKey("PersonnelId")]
        public virtual  Personnel Personnel { get; set; }      
        public virtual File File { get; set; }
        public virtual TrainArticle TrainArticle { get; set; }
        public string Ip { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
