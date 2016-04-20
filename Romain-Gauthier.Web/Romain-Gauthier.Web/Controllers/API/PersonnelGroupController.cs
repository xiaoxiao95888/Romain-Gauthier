using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class PersonnelGroupController : BaseApiController
    {
        private readonly IPersonnelGroupService _personnelGroupService;
        public PersonnelGroupController(IPersonnelGroupService personnelGroupService)
        {
            _personnelGroupService = personnelGroupService;
        }
        public object Get()
        {
            var model = _personnelGroupService.GetPersonnelGroups().ToArray().Select(n => new PersonnelGroupModel
            {
                Id = n.Id,
                Name = n.Name,
                Description = n.Description,
                TrainArticleModels = n.TrainArticles.Select(p => new TrainArticleModel
                {
                    Id = p.Id,
                    Title = p.Title
                }).ToArray(),
                ProductTypeModels = n.ProductTypes.Select(p => new ProductTypeModel
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToArray()
            }).ToArray();
            return model;
        }
        public object Post(PersonnelGroupModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return Failed("名称不得为空");
            }
            var item = _personnelGroupService.GetPersonnelGroup(model.Id);
            if (item != null)
            {
                item.Description = model.Description;
                item.Name = model.Name;
                _personnelGroupService.Update();
            }
            else
            {
                _personnelGroupService.Insert(new PersonnelGroup
                {
                    Id = Guid.NewGuid(),
                    Description = model.Description,
                    Name = model.Name,
                    //TrainArticles = trainArticles,
                    //ProductTypes = productTypes
                });
            }
            return Success();
        }
        public object Put(PersonnelGroupModel model)
        {
            var item = _personnelGroupService.GetPersonnelGroup(model.Id);
            if (item == null || item.IsDeleted)
            {
                return Failed("找不到用户组");
            }
            var productTypeIds = model.ProductTypeModels.Select(n => n.Id).ToList();
            var productTypes = _personnelGroupService.GetProductTypes().Where(n => productTypeIds.Contains(n.Id)).ToList();
            var trainArticleIds = model.TrainArticleModels.Select(n => n.Id).ToList();
            var trainArticles = _personnelGroupService.GetTrainArticles().Where(n => trainArticleIds.Contains(n.Id)).ToList();
            item.ProductTypes.Clear();
            item.TrainArticles.Clear();
            item.ProductTypes = productTypes;
            item.TrainArticles = trainArticles;
            item.Description = model.Description;
            item.Name = model.Name;
            _personnelGroupService.Update();
            return Success();
        }
        public object Delete(Guid id)
        {
            var item = _personnelGroupService.GetPersonnelGroup(id);
            if (item == null)
            {
                return Failed("找不到用户组");
            }
            if (item.Personnels.Any(n => !n.IsDeleted))
            {
                return Failed("删除失败，该用户组中存在有效用户");
            }
            item.IsDeleted = true;
            _personnelGroupService.Update();
            return Success();
        }
    }
}
