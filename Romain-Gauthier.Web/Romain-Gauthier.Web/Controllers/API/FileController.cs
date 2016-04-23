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
    public class FileController : BaseApiController
    {
        private readonly IFileService _fileService;
        
        public FileController(IFileService fileService)
        {           
            _fileService = fileService;
        }

        public object Get()
        {
            return null;
            //var currentPersonnelModel = HttpContext.Current.User.Identity.GetUser();
            //var currentUser = _personnelService.GetPersonnel(currentPersonnelModel.Id);
            //var trainArticles = currentUser.PersonnelGroups.SelectMany(n => n.TrainArticles).Distinct();
            //var model = trainArticles.Select(n => new TrainArticleModel
            //{
            //    Id = n.Id,
            //    //Content = n.Content,
            //    Thumbnail = n.Thumbnail,
            //    Title = n.Title
            //}).ToArray();
            //return model;
        }
    }
}
