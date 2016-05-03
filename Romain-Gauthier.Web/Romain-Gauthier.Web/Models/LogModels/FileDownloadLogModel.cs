using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models.LogModels
{
    public class FileDownloadLogModel
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public Guid? ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public Guid PersonnelId { get; set; }
        public string PersonnelName { get; set; }
        public DateTime Date { get; set; }
    }
}