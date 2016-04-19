using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models.Interfaces;

namespace Romain_Gauthier.Library.Models
{
    public class Product: IDtStamped
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public virtual ProductType ProductType { get; set; }
        /// <summary>
        /// 产品对于的图集
        /// </summary>
        public virtual ICollection<File> Files { get; set; } 
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
