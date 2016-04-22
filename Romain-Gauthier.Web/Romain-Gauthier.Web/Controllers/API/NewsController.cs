using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class NewsController : BaseApiController
    {
        private readonly INewsService _nwesService;
        public NewsController(INewsService nwesService)
        {
            _nwesService = nwesService;
        }

        public object Get()
        {
            var searchkey = HttpContext.Current.Request["searchkey"] ?? "";

            var model =
                _nwesService.GetNewses()
                    .Where(
                        n =>
                            n.Title.Contains(searchkey) || n.Description.Contains(searchkey) ||
                            n.NewsType.Name.Contains(searchkey))
                    .Select(n => new NewsModel
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Content = n.Content,
                        Description = n.Description,
                        IsPublish = n.IsPublish,
                        NewsTypeName = n.NewsType.Name,
                        NewsTypeId = n.NewsTypeId,
                        Thumbnail = n.Thumbnail,
                        UpdateTime = n.UpdateTime
                    }).ToArray();
            return model;
        }

        public object Post(NewsModel model)
        {
            if (model.Title != null && model.Description != null && model.NewsTypeId != null && model.Content != null)
            {
                _nwesService.Insert(new News
                {
                    Id = Guid.NewGuid(),
                    IsPublish = model.IsPublish,
                    Content = model.Content,
                    Description = model.Description,
                    NewsTypeId = model.NewsTypeId,
                    Title = model.Title,
                    Thumbnail = model.Thumbnail
                });
                return Success();
            }
            else
            {
                return Failed("操作失败");
            }
        }

        public object Put(NewsModel model)
        {
            var item = _nwesService.GetNews(model.Id);
            if (model.Title != null && model.Description != null && model.NewsTypeId != null && model.Content != null &&
                item != null)
            {
                item.Title = model.Title;
                item.Content = model.Content;
                item.Description = model.Description;
                item.IsPublish = model.IsPublish;
                item.Thumbnail = model.Thumbnail;
                item.NewsTypeId = model.NewsTypeId;
                _nwesService.Update();
                return Success();
            }
            return Failed("操作失败");
        }

        public object Delete(Guid id)
        {
            var item = _nwesService.GetNews(id);
            if (item != null)
            {
                item.IsDeleted = true;
                _nwesService.Update();
                return Success();
            }
            return Failed("操作失败");
        }

        public object Get(Guid id)
        {
            var item = _nwesService.GetNews(id);
            return new NewsModel
            {
                Title = item.Title,
                Content = item.Content,
                Description = item.Description,
                IsPublish = item.IsPublish,
                NewsTypeName = item.NewsType.Name,
                NewsTypeId = item.NewsTypeId,
                Thumbnail = item.Thumbnail,
                UpdateTime = item.UpdateTime
            };
        }
    }
    [AllowAnonymous]
    public class NewsPublishController : BaseApiController
    {
        private readonly INewsService _nwesService;
        public NewsPublishController(INewsService nwesService)
        {
            _nwesService = nwesService;
        }

        public object Get()
        {
            var searchkey = HttpContext.Current.Request["searchkey"] ?? "";
            var model =
                _nwesService.GetNewses()
                    .Where(
                        n =>
                            n.Title.Contains(searchkey) || n.Description.Contains(searchkey) ||
                            n.NewsType.Name.Contains(searchkey)).Where(n => n.IsPublish)
                    .Select(n => new NewsModel
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Content = n.Content,
                        Description = n.Description,
                        IsPublish = n.IsPublish,
                        NewsTypeName = n.NewsType.Name,
                        NewsTypeId = n.NewsTypeId,
                        Thumbnail = n.Thumbnail,
                        UpdateTime = n.UpdateTime
                    }).ToArray().GroupBy(n => n.NewsTypeId).ToArray();
            return model;
        }
    }
}
