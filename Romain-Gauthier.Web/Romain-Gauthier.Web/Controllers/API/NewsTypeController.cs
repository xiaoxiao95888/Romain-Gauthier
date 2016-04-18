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
    public class NewsTypeController : BaseApiController
    {
        private readonly INewsTypeService _nwesTypeService;
        public NewsTypeController(INewsTypeService nwesTypeService)
        {
            _nwesTypeService = nwesTypeService;
        }

        public object Get()
        {
            var model = _nwesTypeService.GetNewsTypes().Select(n => new NewsTypeModel
            {
                Id = n.Id,
                Name = n.Name
            }).ToArray();
            return model;
        }
    }
}
