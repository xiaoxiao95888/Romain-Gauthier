using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Romain_Gauthier.Web.Models;

namespace Romain_Gauthier.Web.Controllers.API
{
    [Authorize]
    public class BaseApiController : ApiController
    {
        protected ResponseModel Success(string message = null)
        {
            return new ResponseModel
            {
                ErrorCode = 0,
                Message = message,
                Error = false
            };
        }
        protected ResponseModel Failed(string message = null)
        {
            return new ResponseModel
            {
                ErrorCode = 0,
                Message = message,
                Error = true
            };
        }
    }
}
