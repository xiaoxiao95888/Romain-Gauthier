using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models.Interfaces;

namespace Romain_Gauthier.Library.Models
{
    public class ViewRecord : IDtStamped
    {
        public Guid Id { get; set; }
        public virtual  Personnel Personnel { get; set; }
        public string Ip { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
