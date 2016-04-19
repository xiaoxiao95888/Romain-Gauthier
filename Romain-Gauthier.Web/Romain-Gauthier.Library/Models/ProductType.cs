using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models.Interfaces;

namespace Romain_Gauthier.Library.Models
{
    public class ProductType : IDtStamped
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual ProductType Parent{ get; set; }
        public virtual ICollection<Product> Products { get; set; } 
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
