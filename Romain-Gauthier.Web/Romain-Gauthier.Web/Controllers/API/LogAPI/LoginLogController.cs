using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models.LogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Romain_Gauthier.Web.Controllers.API.LogAPI
{
    public class LoginLogController : BaseApiController
    {
        private readonly ILoginLogService _loginLogService;
        public LoginLogController(ILoginLogService loginLogService)
        {
            _loginLogService = loginLogService;
        }
        public object Get()
        {
            var fromdate = string.IsNullOrEmpty(HttpContext.Current.Request["FromDate"]) ? DateTime.MinValue : Convert.ToDateTime(HttpContext.Current.Request["FromDate"]);
            var todate = string.IsNullOrEmpty(HttpContext.Current.Request["ToDate"]) ? DateTime.MinValue : Convert.ToDateTime(HttpContext.Current.Request["ToDate"]).AddDays(1);
            var model = _loginLogService.GetLoginlogs().Where(n => n.UpdateTime >= fromdate && n.UpdateTime < todate).Select(n => new LoginLogModel
            {
                PersonnelId = n.Personnel.Id,
                PersonnelName = n.Personnel.Name,
                Date = n.UpdateTime.Value

            }).OrderByDescending(n => n.Date).ToArray();
            return model;
        }
    }
}
