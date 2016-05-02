using Romain_Gauthier.Library.Models.Enum;
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
    public class PersonnelImageController : BaseApiController
    {
        private readonly IPersonnelService _personnelService;
        private readonly IFileService _fileService;
        public PersonnelImageController(IPersonnelService personnelService, IFileService fileService)
        {
            _personnelService = personnelService;
            _fileService = fileService;
        }
        public object Get()
        {
            var openId = HttpContext.Current.User.Identity.Name;
            var currentUser = _personnelService.GetPersonnelByOpenId(openId);
            var model = currentUser.PersonnelGroups.SelectMany(n => n.ProductTypes).SelectMany(n => n.Products).Where(n => !n.IsDeleted).SelectMany(n => n.Files).Where(n => !n.IsDeleted).Where(n => n.FileType == FileType.图片 && n.ProductId != null).OrderByDescending(n => n.UpdateTime).Distinct().Select(n => new ProductImageModel
            {
                Id = n.Id,
                FileName = n.FileName,
                ProductId = n.ProductId,
                ProductName = n.Product.Name,
                ProductTypeId=n.ProductId,                
                UpdateTime = n.UpdateTime,
                ProductTypeName = n.Product.ProductType.Name,
                Name = n.Name,
                Thumbnail=n.Thumbnail
            }).ToArray().GroupBy(n => n.ProductId).Select(n => new
            {
                Key = n.Key,
                ProductTypeName = n.Select(p=>p.ProductTypeName).FirstOrDefault(),
                Items = n.ToArray()
            }).ToArray();

            return model;
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public object Post(ProductImageModel model)
        {
            var item = _fileService.GetFile(model.Id);
            if (item != null)
            {
                var fileUrl = ConfigurationManager.AppSettings["FilePath"];
                
                var openId = HttpContext.Current.User.Identity.Name;
                var currentUser = _personnelService.GetPersonnelByOpenId(openId);
                //记录下载次数
                item.ViewRecords.Add(new Library.Models.ViewRecord
                {
                    Id = Guid.NewGuid(),
                    Ip = GetIP(),
                    PersonnelId = currentUser.Id
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
