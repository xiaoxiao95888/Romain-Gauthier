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
            get { return Thumbnail != null ? ConfigurationManager.AppSettings["FileUrl"] + Thumbnail : null; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        public string Flag
        {
            get
            {
                var date = DateTime.Now;
                var timedifference = date - UpdateTime.Value;
                string str;
                if (timedifference.TotalHours > 24)
                {
                    str = string.Format("{0} {1} ago {2}", (int)timedifference.TotalDays, "days", NewsTypeName);
                }
                else if (timedifference.TotalMinutes > 60)
                {
                    str = string.Format("{0} {1} ago {2}", (int)timedifference.TotalHours, "hours", NewsTypeName);
                }
                else
                {
                    str = string.Format("{0} {1} ago {2}", (int)timedifference.TotalMinutes, "minutes", NewsTypeName);
                }
                return str;
            }
        }

        public Guid? NewsTypeId { get; set; }
        public string NewsTypeName { get; set; }
        public bool IsPublish { get; set; }
        public DateTime? UpdateTime { get; set; }
       
    }
}