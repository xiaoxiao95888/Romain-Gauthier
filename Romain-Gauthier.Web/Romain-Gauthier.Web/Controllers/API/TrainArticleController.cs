﻿using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class TrainArticleController : BaseApiController
    {
        private readonly ITrainArticleService _trainArticleService;
        public TrainArticleController(ITrainArticleService trainArticleService)
        {
            _trainArticleService = trainArticleService;
        }
        public object Get()
        {
            var model = _trainArticleService.GetTrainArticles().Select(n => new TrainArticleModel
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content

            }).ToArray();
            return model;
        }
        public object Post(TrainArticleModel model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                return Failed("标题不得为空");
            }
            _trainArticleService.Insert(new TrainArticle
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Content = model.Content,
                Thumbnail=model.Thumbnail
            });
            return Success();
        }
        public object Put(TrainArticleModel model)
        {
            var item = _trainArticleService.GetTrainArticle(model.Id);
            if(item==null || item.IsDeleted)
            {
                return Failed("无法找到该次培训");
            }
            item.Title = model.Title;
            item.Content = model.Content;
            item.Thumbnail = model.Thumbnail;
            _trainArticleService.Update();
            return Success();
        }
        public object Delete(Guid id)
        {
            var item = _trainArticleService.GetTrainArticle(id);
            if (item == null || item.IsDeleted)
            {
                return Failed("无法找到该次培训");
            }           
            item.IsDeleted = true;
            item.TrainQuestions.ForEach(n => n.IsDeleted = true);
            _trainArticleService.Update();
            return Success();
        }
    }
}