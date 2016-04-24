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
                //Content = n.Content,
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
                    Title = trainArticles.Title
                };
            }
            return null;
        }
    }
}
