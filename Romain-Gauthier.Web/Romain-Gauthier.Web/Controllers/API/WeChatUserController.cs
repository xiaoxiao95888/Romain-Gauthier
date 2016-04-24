using Romain_Gauthier.Web.Infrastructure;
using Romain_Gauthier.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class WeChatUserController : ApiController
    {
        public object Get()
        {
            var openId = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(openId))
            {
                return new ResponseModel
                {
                    ErrorCode = 0,
                    Error = true
                };
            }
            else
            {
                return new ResponseModel
                {
                    ErrorCode = 0,
                    Error = false
                };

            }            
        }
    }
}
