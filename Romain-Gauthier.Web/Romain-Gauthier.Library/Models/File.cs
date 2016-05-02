using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models.Enum;
using Romain_Gauthier.Library.Models.Interfaces;

namespace Romain_Gauthier.Library.Models
{
    public class File : IDtStamped
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public Guid? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual  Product Product { get; set; }
        public Guid? PersonnelGroupId { get; set; }
        [ForeignKey("PersonnelGroupId")]
        public virtual PersonnelGroup PersonnelGroup { get; set; }
        public string Thumbnail { get; set; }
        /// <summary>
        /// 下载记录
        /// </summary>
        public virtual ICollection<ViewRecord> ViewRecords { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
