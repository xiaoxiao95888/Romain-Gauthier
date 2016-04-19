using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Romain_Gauthier.Web.Models
{
    public class PersonnelGroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductTypeModel[] ProductTypeModels { get; set; }
        public TrainArticleModel[] TrainArticleModels { get; set; }
    }
}