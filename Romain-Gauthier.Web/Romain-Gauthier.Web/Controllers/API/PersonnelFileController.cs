using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Infrastructure.Utilites;
using Romain_Gauthier.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class PersonnelFileController : BaseApiController
    {
        private readonly IPersonnelService _personnelService;
        private readonly IFileService _fileService;
        public PersonnelFileController(IPersonnelService personnelService, IFileService fileService)
        {
            _personnelService = personnelService;
            _fileService = fileService;
        }
        public object Get()
        {
            var openId = HttpContext.Current.User.Identity.Name;
            var currentUser = _personnelService.GetPersonnelByOpenId(openId);
            var model = currentUser.PersonnelGroups.SelectMany(n => n.Files).Where(n => !n.IsDeleted).OrderByDescending(n => n.UpdateTime).Distinct().Select(n => new FileModel
            {
                Id = n.Id,
                FileName = n.FileName,
                UpdateTime = n.UpdateTime,
                Name = n.Name,
            }).ToArray();

            return model;
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="id">file id</param>
        /// <returns></returns>
        public object Post(Guid id)
        {
            var item = _fileService.GetFile(id);
            if (item != null)
            {
                var fileUrl = ConfigurationManager.AppSettings["FilePath"];
                var openId = HttpContext.Current.User.Identity.Name;
                var currentUser = _personnelService.GetPersonnelByOpenId(openId);
                //记录下载次数
                item.ViewRecords.Add(new Library.Models.ViewRecord
                {
                    Id=Guid.NewGuid(),
                    Ip= GetIP(),
                    PersonnelId=currentUser.Id
                });
                _fileService.Update();
                //send email
                MailHelp.SendMailForImage(currentUser.Email, currentUser.Name, fileUrl + item.FileName);
                return Success();
            }
            return Failed();

        }
        private string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }
    }
}
