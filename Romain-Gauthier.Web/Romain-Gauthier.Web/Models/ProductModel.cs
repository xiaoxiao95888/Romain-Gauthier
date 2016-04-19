using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? TypeId { get; set; }
        public string TypeName { get; set; }
    }
}