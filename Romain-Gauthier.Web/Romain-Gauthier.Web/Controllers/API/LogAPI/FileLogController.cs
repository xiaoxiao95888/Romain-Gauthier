using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models.LogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Romain_Gauthier.Web.Controllers.API.LogAPI
{
    public class FileLogController : BaseApiController
    {
        private readonly IViewRecordService _viewRecordService;
        public FileLogController(IViewRecordService viewRecordService)
        {
            _viewRecordService = viewRecordService;
        }
        public object Get()
        {
            var fromdate = string.IsNullOrEmpty(HttpContext.Current.Request["FromDate"]) ? DateTime.MinValue : Convert.ToDateTime(HttpContext.Current.Request["FromDate"]);
            var todate = string.IsNullOrEmpty(HttpContext.Current.Request["ToDate"]) ? DateTime.MinValue : Convert.ToDateTime(HttpContext.Current.Request["ToDate"]).AddDays(1);
            var model = _viewRecordService.GetViewRecords().Where(n => n.File != null && n.UpdateTime >= fromdate && n.UpdateTime < todate).Select(n => new FileDownloadLogModel
            {
                FileId = n.File.Id,
                FileName = n.File.Name,
                ProductTypeId = n.File.ProductId,
                ProductTypeName = n.File.Product != null ? n.File.Product.ProductType.Name : null,
                PersonnelId = n.Personnel.Id,
                PersonnelName = n.Personnel.Name,
                Date=n.UpdateTime.Value
            }).OrderByDescending(n => n.Date).ToArray();
            return model;
        }
    }
}
