using Romain_Gauthier.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class ProductTypeGroupController : BaseApiController
    {
        private readonly IPersonnelService _personnelService;
        public ProductTypeGroupController(IPersonnelService personnelService)
        {           
            _personnelService = personnelService;
        }
        public object Get()
        {
            var openId = HttpContext.Current.User.Identity.Name;
            var currentUser = _personnelService.GetPersonnelByOpenId(openId);
            //可访问的产品系列Index
            var model = currentUser.PersonnelGroups.SelectMany(n => n.ProductTypes).Select(n => n.Index).Distinct().ToArray();          
            return model;
        }
    }
}
