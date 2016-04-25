using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models
{
    public class TrainArticleModel
    {        
        public Guid Id { get; set; }
        public string Thumbnail { get; set; }
        public int? Index { get; set; }
        public string Url
        {
            get
            {
                var fileUrl = ConfigurationManager.AppSettings["FileUrl"];
                return fileUrl + Thumbnail;
            }
        }
        public string Title { get; set; }
        /// <summary>
        /// 考题前的提示内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 培训的内容
        /// </summary>
        public string TrainContent { get; set; }
    }
}