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
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}