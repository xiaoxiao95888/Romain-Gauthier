using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;
using WebGrease.Css.Extensions;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class ProductTypeController : BaseApiController
    {
        private readonly IProductTypeService _productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        public object Get()
        {
            var model = _productTypeService.GetProductTypes().Select(n => new ProductTypeModel
            {
                Id = n.Id,
                Name = n.Name,
                ParentId = n.ParentId,
                ParentName = n.Parent != null ? n.Parent.Name : string.Empty
            }).ToArray();
            return model;
        }

        public object Post(ProductTypeModel model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                if (model.ParentId != null && model.ParentId == model.Id)
                {
                    return Failed("上级不能与选择自己");
                }
                _productTypeService.Insert(new ProductType
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    ParentId = model.ParentId
                });
                return Success();
            }
            return Failed("名称不能为空");
        }
        public object Put(ProductTypeModel model)
        {
            var item = _productTypeService.GetProductType(model.Id);
            if (item != null)
            {
                item.Name = model.Name;
                item.ParentId = model.ParentId;
                _productTypeService.Update();
                return Success();
            }
            return Failed("找不到该系列");
        }

        public object Delete(Guid id)
        {
            var item = _productTypeService.GetProductType(id);
            if (item != null)
            {
                if (item.Products.Any(n => n.IsDeleted == false || n.Files.Any(p => p.IsDeleted == false)) ||
                    _productTypeService.GetProductTypes().Any(n => n.ParentId == item.Id))
                {
                    return Failed("删除失败，该系列下包含未删除的产品或者未删除的图片或者被指定为其他系列的父类");
                }
                item.IsDeleted = true;
                _productTypeService.Update();
                return Success();
            }
            return Failed("找不到该类型");
        }
    }
}
