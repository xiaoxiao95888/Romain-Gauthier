using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models
{
    public class ProductTypeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Index { get; set; }
        public Guid? ParentId { get; set; }
        public string ParentName { get; set; }
    }
}