using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models
{
    public class ProductImageModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Url
        {
            get
            {
                var fileUrl = ConfigurationManager.AppSettings["FileUrl"];
                return fileUrl + FileName;
            }
        }
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}