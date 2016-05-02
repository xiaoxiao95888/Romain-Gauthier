using Romain_Gauthier.Library.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romain_Gauthier.Library.Models
{
    public class LoginLog : IDtStamped
    {
        public Guid Id { get; set; }
        public Guid? PersonnelId { get; set; }
        [ForeignKey("PersonnelId")]
        public virtual Personnel Personnel { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
