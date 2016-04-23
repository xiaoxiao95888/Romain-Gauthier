using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;

namespace Romain_Gauthier.Web.Controllers.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private readonly IPersonnelService _personnelService;
        public LoginController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }
        public object Post(PersonnelModel model)
        {
            if (model == null)
            {
                return new ResponseModel
                {
                    ErrorCode = 0,
                    Error = true
                };
            }
            var item = _personnelService.GetPersonnelByOpenId(model.OpenId);
            if (item == null)
            {
                return new ResponseModel
                {
                    ErrorCode = 0,
                    Error = true
                };
            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.OpenId.ToString(), false);
                return new ResponseModel
                {
                    ErrorCode = 0,
                    Error = false
                };
            }
        }
    }
}
