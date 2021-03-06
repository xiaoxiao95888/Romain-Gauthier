﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Romain_Gauthier.Web.Models;
using Romain_Gauthier.Library.Services;
using System.Web.Security;
using Romain_Gauthier.Library.Models.Enum;
using System.Web.Http.Cors;

namespace Romain_Gauthier.Web.Controllers.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BindLicenseController : ApiController
    {
        private readonly IPersonnelService _personnelService;
        public BindLicenseController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }
        public object Post(PersonnelModel model)
        {
            if (string.IsNullOrEmpty(model.License))
            {
                return new ResponseModel
                {
                    ErrorCode = 0,
                    Error = true
                };
            }
            var license = model.License.Trim().ToUpper();
            var item = _personnelService.GetPersonnels().Where(n=>n.License== license && n.IsBindLicense==false).FirstOrDefault();
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
                item.IsBindLicense = true;
                item.Headimgurl = model.Headimgurl;
                item.Gender = (Gender?)model.Gender;
                item.City = model.City;
                item.Country = model.Country;
                item.Language = model.Language;
                item.NickName = model.NickName;
                item.OpenId = model.OpenId;
                item.Province = model.Province;
                _personnelService.Update();
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
