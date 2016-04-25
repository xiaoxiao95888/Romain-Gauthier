using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using Romain_Gauthier.Web.Models.Enum;

namespace Romain_Gauthier.Web.Models
{
    public class FileModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? PersonnelGroupId { get; set; }
        public string PersonnelGroupName { get; set; }
        public string Url
        {
            get
            {
                var fileUrl = ConfigurationManager.AppSettings["FileUrl"];
                return fileUrl + FileName;
            }
        }
        public string Flag
        {
            get
            {
                var date = DateTime.Now;
                var timedifference = date - (UpdateTime ?? DateTime.Now);
                string str;
                if (timedifference.TotalHours > 24)
                {
                    str = string.Format("{0} {1} ago", (int)timedifference.TotalDays, "days");
                }
                else if (timedifference.TotalMinutes > 60)
                {
                    str = string.Format("{0} {1} ago", (int)timedifference.TotalHours, "hours");
                }
                else
                {
                    str = string.Format("{0} {1} ago", (int)timedifference.TotalMinutes, "minutes");
                }
                return str;
            }
        }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}