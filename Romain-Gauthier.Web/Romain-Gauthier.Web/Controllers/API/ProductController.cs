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
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        public ProductController(IProductService productService, IProductTypeService productTypeService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
        }

        public object Get()
        {
            var model = _productService.GetProducts().OrderBy(n=>n.ProductTypeId).ThenByDescending(n=>n.UpdateTime).Select(n => new ProductModel
            {
                Id = n.Id,
                Name = n.Name,
                TypeId = n.ProductTypeId,
                TypeName = n.ProductType.Name

            }).ToArray();
            return model;
        }

        public object Post(ProductModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || model.TypeId == null)
            {
                return Failed("操作失败");
            }
            _productService.Insert(new Product
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                ProductTypeId = model.TypeId.Value
            });
            return Success();
        }

        public object Put(ProductModel model)
        {
            var item = _productService.GetProduct(model.Id);
            if (item != null)
            {
                if (model.TypeId == null || _productTypeService.GetProductType(model.TypeId.Value) == null)
                {
                    return Failed("找不到该系列");
                }
                item.Name = model.Name;
                item.ProductTypeId = model.TypeId.Value;
                return Success();
            }
            return Failed("找不到该产品");
        }

        public object Delete(Guid id)
        {
            var item = _productService.GetProduct(id);
            if (item != null)
            {
                if (item.Files.Any(n => n.IsDeleted == false))
                {
                    return Failed("删除失败，该产品下包含未删除的图片");
                }
                item.IsDeleted = true;
                _productService.Update();
                return Success();
            }
            return Failed("找不到该产品");
        }
    }
}
