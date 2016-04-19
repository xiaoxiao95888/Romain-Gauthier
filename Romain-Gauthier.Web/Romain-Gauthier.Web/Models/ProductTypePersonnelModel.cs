using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models
{
    public class ProductTypePersonnelModel
    {
        public Guid ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public Guid PersonnelId { get; set; }
        public string PersonnelName { get; set; }
    }
}