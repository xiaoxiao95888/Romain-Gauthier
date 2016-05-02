using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;
using Romain_Gauthier.Web.Infrastructure;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class PersonnelTrainArticleController : BaseApiController
    {
        private readonly ITrainArticleService _trainArticleService;
        private readonly IPersonnelService _personnelService;
        public PersonnelTrainArticleController(ITrainArticleService trainArticleService, IPersonnelService personnelService)
        {
            _trainArticleService = trainArticleService;
            _personnelService = personnelService;
        }

        public object Get()
        {
            var openId = HttpContext.Current.User.Identity.Name;
            var currentUser = _personnelService.GetPersonnelByOpenId(openId);
            var trainArticles = currentUser.PersonnelGroups.SelectMany(n => n.TrainArticles).Where(n=>!n.IsDeleted).Distinct().OrderBy(n=>n.Index);
            var model = trainArticles.Select(n => new TrainArticleModel
            {
                Id = n.Id,
                Thumbnail = n.Thumbnail,
                Title = n.Title
            }).ToArray();
            return model;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">trainArticle id</param>
        /// <returns></returns>
        public object Get(Guid id)
        {
            var openId = HttpContext.Current.User.Identity.Name;
            var currentUser = _personnelService.GetPersonnelByOpenId(openId);
            var trainArticles = currentUser.PersonnelGroups.SelectMany(n => n.TrainArticles).Distinct().FirstOrDefault(n => n.Id == id);
            if (trainArticles != null && trainArticles.IsDeleted == false)
            {               
                return new TrainArticleModel
                {
                    Id = trainArticles.Id,
                    Content = trainArticles.Content,
                    Thumbnail = trainArticles.Thumbnail,
                    Title = trainArticles.Title,                   
                };
            }
            return null;
        }
       
    }
    /// <summary>
    /// 获取详细的培训内容
    /// </summary>
    public class PersonnelTrainArticleContentController : BaseApiController
    {
        private readonly ITrainArticleService _trainArticleService;
        private readonly IPersonnelService _personnelService;
        public PersonnelTrainArticleContentController(ITrainArticleService trainArticleService, IPersonnelService personnelService)
        {
            _trainArticleService = trainArticleService;
            _personnelService = personnelService;
        }      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">trainArticle id</param>
        /// <returns></returns>
        public object Get(Guid id)
        {
            var openId = HttpContext.Current.User.Identity.Name;
            var currentUser = _personnelService.GetPersonnelByOpenId(openId);
            var trainArticles = currentUser.PersonnelGroups.SelectMany(n => n.TrainArticles).Distinct().FirstOrDefault(n => n.Id == id);
            if (trainArticles != null && trainArticles.IsDeleted == false)
            {
                //记录浏览记录
                trainArticles.ViewRecords.Add(new Library.Models.ViewRecord
                {
                    Id = Guid.NewGuid(),
                    Ip = GetIP(),
                    PersonnelId = currentUser.Id
                });
                _personnelService.Update();
                return new TrainArticleModel
                {
                    Id = trainArticles.Id,                   
                    Thumbnail = trainArticles.Thumbnail,
                    Title = trainArticles.Title,
                    TrainContent = trainArticles.TrainContent
                };
            }
            return null;
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
