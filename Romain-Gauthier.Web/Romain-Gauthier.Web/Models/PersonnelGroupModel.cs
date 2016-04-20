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
        public string ProductTypes
        {
            get { return string.Join(",", ProductTypeModels.Select(n => n.Name)); }
        }
       
        public TrainArticleModel[] TrainArticleModels { get; set; }
        public string TrainArticles
        {
            get { return string.Join(",", TrainArticleModels.Select(n => n.Title)); }
        }
    }
}