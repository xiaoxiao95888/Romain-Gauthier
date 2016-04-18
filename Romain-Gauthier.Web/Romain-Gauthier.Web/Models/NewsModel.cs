using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models
{
    public class NewsModel
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

        public string ThumbnailUrl
        {
            get { return Thumbnail != null ? ConfigurationManager.AppSettings["adminemail"] + Thumbnail : null; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        public Guid? NewsTypeId { get; set; }
        public string NewsTypeName { get; set; }
        public bool IsPublish { get; set; }
        public DateTime? UpdateTime { get; set; }
       
    }
}