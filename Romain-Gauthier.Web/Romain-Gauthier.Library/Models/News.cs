using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models.Interfaces;

namespace Romain_Gauthier.Library.Models
{
    public class News : IDtStamped
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        public Guid? NewsTypeId { get; set; }
        [ForeignKey("NewsTypeId")]
        public virtual  NewsType NewsType { get; set; }
        public bool IsPublish { get; set; }
        public string ExternalUrl { get; set; }
        /// <summary>
        /// 浏览记录
        /// </summary>
        public virtual ICollection<ViewRecord> ViewRecords { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
