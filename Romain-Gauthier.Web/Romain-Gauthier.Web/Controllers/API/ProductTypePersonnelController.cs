using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class ProductTypePersonnelController : BaseApiController
    {
        private readonly IProductTypeService _productTypeService;
        public ProductTypePersonnelController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ProductType Id</param>
        /// <returns></returns>
        public object Get(Guid id)
        {
            var productType = _productTypeService.GetProductType(id);
            if (productType != null)
            {
                var model = productType.Personnels.Select(n => new ProductTypePersonnelModel
                {
                    PersonnelId = n.Id,
                    PersonnelName = n.Name,
                    ProductTypeId = productType.Id,
                    ProductTypeName = productType.Name
                }).ToArray();
                return model;
            }
            return Failed("找不到该产品系列");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">productType Id</param>
        /// <param name="ids">Personnel Ids</param>
        /// <returns></returns>
        public object Post(Guid id, Guid[] ids)
        {
            var productType = _productTypeService.GetProductType(id);
            if (productType != null)
            {
                var personnels = _productTypeService.GetPersonnels().Where(n => ids.Contains(n.Id)).ToArray();
                foreach (var item in personnels)
                {
                    productType.Personnels.Add(item);
                }
                _productTypeService.Update();
                return Success();
            }
            return Failed("找不到该产品系列");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">productType Id</param>
        /// <param name="ids">Personnel Ids</param>
        /// <returns></returns>
        public object Delete(Guid id, Guid[] ids)
        {
            var productType = _productTypeService.GetProductType(id);
            if (productType != null)
            {
                var personnels = _productTypeService.GetPersonnels().Where(n => ids.Contains(n.Id)).ToArray();
                foreach (var item in personnels)
                {
                    productType.Personnels.Remove(item);
                }
                _productTypeService.Update();
                return Success();
            }
            return Failed("找不到该产品系列");
        }
    }
}
